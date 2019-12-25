from VNlp.VNlp import VNlp
from data_utils.data_utils import DataUtils
import numpy as np
from textrank.textrank import TextRank

with open('vi.txt', 'r') as f:
    data = f.read()
# data = "Đất nước. Nước mắt"
control = VNlp()
control.from_bin('./VNlp/wiki.vi.model')
 
utils = DataUtils()
data = utils.nomalize_document(data)
sent_list = utils.split_sentences(data)

print(sent_list)

sent_vec = []
for i, sent in enumerate(sent_list):
    sent_vec.append(control.to_vector(sent.lower()))

sent_size = len(sent_vec)   
cosines = np.zeros((sent_size, sent_size), dtype=float) 
for i in range(sent_size):
    for j in range(sent_size):
        if i != j:
            cosines[i][j] = control.cosine_distance(sent_vec[i], sent_vec[j])
rank = TextRank()
rank.run(cosines)

for i in list(rank.sorted_scores.keys())[:3]:
    print(sent_list[i])

# print(type(rank.sorted_scores.keys()))
