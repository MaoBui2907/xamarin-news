import re
import underthesea


class DataUtils:
    def __init__(self, lang='vi'):
        self.lang = lang
        self.control = underthesea
        pass

    def nomalize_document(self, document, pattern=" +", repl=" ", reverse=False):
        return re.sub(pattern, repl, document.strip())

    def split_sentences(self, document, sent_split=[".", "\n", "!", "?"]):
        sent_pattern = "[" + "".join(sent_split) + "]"
        return list(filter(lambda x: x not in [' ', ''], re.split(sent_pattern, document)))

    def tokenize(self, sent, black_list=[]):

        return self.control.word_tokenize(sent)

    def pos_tag(self, sent, black_list=[]):
        return [(t, p) for t, p in self.control.pos_tag(sent) if p not in black_list]

    def chunk_tokenize(self, sent):
        return self.control.chunk(sent)