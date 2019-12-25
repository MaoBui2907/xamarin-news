import plac
import numpy as np
from numpy.linalg import norm
import pickle

import spacy
from spacy.language import Language


class VNlp:
    def __init__(self, lang="en"):
        self.nlp = spacy.blank(lang)

    def load_model(self, model):
        self.nlp = spacy.load(model)

    def load_copus(self, vectors_corpus):
        with open(vectors_corpus, 'rb') as file_:
            header = file_.readline()
            nr_row, nr_dim = header.split()
            self.nlp.vocab.reset_vectors(width=int(nr_dim))
            for line in file_:
                line = line.rstrip().decode('utf8')
                pieces = line.rsplit(' ', int(nr_dim))
                word = pieces[0]
                vector = np.asarray([float(v)
                                     for v in pieces[1:]], dtype='f')
                # add the vectors to the vocab
                self.nlp.vocab.set_vector(word, vector)

    def cosine_distance(self, vector1, vector2):
        return np.inner(vector1, vector2) / (norm(vector1) * norm(vector2))

    def to_vector(self, sent):
        return self.nlp(sent).vector

    def to_disk(self, path):
        self.nlp.to_disk(path)

    def to_bin(self, path):
        with open(path, 'wb') as f:
            pickle.dump(self.nlp.to_bytes(), f)

    def from_disk(self, path):
        self.nlp.from_disk(path)

    def from_bin(self, path):
        with open(path, 'rb') as f:
            self.nlp.from_bytes(pickle.load(f))


if __name__ == "__main__":
    vector_corpus = ("./corpus/wiki.vi.vec")
    nlp_model = VNlp()
    nlp_model.load_copus(vector_corpus)
    nlp_model.to_bin("wiki.vi.model")
