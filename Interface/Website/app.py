import streamlit
from api_service import api
from streamlit_option_menu import option_menu

def get_categories():
    streamlit.session_state['categories'] = api.get_categories()

def search_news():
    category = streamlit.session_state.get('category', None)
    search_key = streamlit.session_state.get('search', None)
    page = streamlit.session_state.get('page', 1)
    streamlit.session_state['news'] = api.search_news(category, search_key, page)

def set_active_news(news_idx: int = None):
    if news_idx is None:
        news = streamlit.session_state.get('news', [])[0]
        if news is not None:
            streamlit.session_state['active_news_id'] = news.get('id')
        else:
            streamlit.session_state['active_news_id'] = None
    else:
        news = streamlit.session_state.get('news', [])[news_idx]
        streamlit.session_state['active_news_id'] = news.get('id')
    

def read_news():
    news_id = streamlit.session_state.get('active_news_id', None)
    if news_id is not None:
        compress = streamlit.session_state.get('compress_rate', 70)
        news = api.get_news_by_id(news_id, compress / 100)
        streamlit.session_state['active_news'] = news
        
def click_choose_news(key: str):
    displayed = streamlit.session_state.get(key, None)
    if displayed is None:
        return
    news_idx = int(displayed.split('.')[0]) - 1
    streamlit.session_state['menu_index'] = news_idx
    set_active_news(news_idx)
    read_news()

def set_page(page: int):
    streamlit.session_state['page'] = page
    streamlit.session_state['menu_index'] = 0
    
def click_change_page(page: int):
    set_page(page)
    search_news()
    read_news()

def init_app():
    if streamlit.session_state.get('initialized', False):
        return
    print('init app')
    get_categories()
    set_page(1)
    search_news()
    streamlit.session_state['initialized'] = True

def app_configuration():
    if streamlit.session_state.get('configurated', False):
        return
    print('app configuration')
    streamlit.set_page_config(
        page_title="New Summarizer",
        layout="wide",
    )
    streamlit.session_state['configurated'] = True

def run():
    init_app()
    app_configuration()

    left_padding, main, right_padding = streamlit.columns([0.1, 0.8, 0.1])

    with left_padding:
        pass

    with right_padding:
        pass

    with main:
        # Title
        left_padding, title, right_padding = streamlit.columns([0.2, 0.6, 0.2])
        with title:
            streamlit.title("News Summarizer")
        streamlit.empty()

        # Search form
        left_padding, form1, form2, form3, right_padding = streamlit.columns([0.1, 0.3, 0.4, 0.1, 0.1])
        with streamlit.form('News search'):
            with form1:
                streamlit.selectbox(
                    "Select category", 
                    key="category", 
                    placeholder="Select category...", 
                    index=None, 
                    options=streamlit.session_state.get('categories', []),
                    label_visibility="collapsed"
                )
            with form2:
                streamlit.text_input("Search news", key="search", placeholder="Search news...", label_visibility="collapsed")
            with form3:
                streamlit.button("Submit", on_click=search_news)
        streamlit.empty()

        # Compress size
        left_padding, label, slider, right_padding = streamlit.columns([0.1, 0.3, 0.5, 0.1])
        with label:
            streamlit.write("Compress size")
        with slider:
            streamlit.slider("Compress Size", key="compress_rate", on_change=read_news, min_value=0, max_value=100, value=50, step=10, format="%d%%", label_visibility="collapsed")
        streamlit.empty()

        # News list    
        with streamlit.container():
            left, right = streamlit.columns([0.3, 0.7])
            
            with left:
                option_menu(
                    None,
                    key="news_list",
                    options=['{}. {} - {}'.format(i+1, news.get('title', ''), news.get('author', '')) for i, news in enumerate(streamlit.session_state.get('news', []))],
                    on_change=click_choose_news,
                    manual_select=streamlit.session_state.get('menu_index', 0),
                    styles={
                        "container": {
                            "border": "1px solid #ccc",
                            "border-radius": "5px",
                            "padding": "5px",
                        },
                        "nav-link": {
                            "font-family": "sans-serif",
                            "font-size": "12px",
                            "line-height": "1rem",
                            "height": "2.5rem",
                            "text-align": "left",
                            "padding": "4px",
                            "--hover-border": "#ccc",
                        },
                        "nav-link-selected": {
                            "font-family": "sans-serif",
                            "background-color": "var(--background-color)",
                            "border": "1px solid var(--hover-border)",
                        },
                        "icon": {
                            "display": "none"
                        }
                    }
                )
            with right:
                with streamlit.container(border=True):
                    streamlit.write(streamlit.session_state.get('active_news', {}).get('summary', ''))
                    
        with streamlit.container():
            _, back, current, next, _ = streamlit.columns([0.075, 0.05, 0.035, 0.05, 0.79])
            back.button("👈", on_click=click_change_page, args=[streamlit.session_state.get('page', 1) - 1], disabled=streamlit.session_state.get('page', 1) == 1)
            current.button("{}".format(streamlit.session_state.get('page', 1)), disabled=True)
            next.button("👉", on_click=click_change_page, args=[streamlit.session_state.get('page', 1) + 1])
                


if __name__ == "__main__":
    run()