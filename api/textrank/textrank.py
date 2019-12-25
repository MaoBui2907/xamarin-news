import networkx as nx
from operator import itemgetter

class TextRank:
    def __init__(self):

        pass

    def run(self, score):
        self.graph = nx.from_numpy_array(score)
        self.scores = nx.pagerank(self.graph,max_iter=500)
        self.sorted_scores = dict(sorted(self.scores.items(), key=itemgetter(1), reverse= True))
        pass

