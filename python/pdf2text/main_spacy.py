import spacy
from spacy import displacy
from spacy.lang.it.examples import sentences

# nlp = spacy.load("it_core_news_sm")
nlp = spacy.load("en_core_web_sm")

# doc = nlp(sentences[0])
# print(doc.text)
# for token in doc:
#     print(token.text, token.pos_, token.dep_)


raw_text="The Indian Space Research Organisation or is the national space agency of India, headquartered in Bengaluru. It operates under Department of Space which is directly overseen by the Prime Minister of India while Chairman of ISRO acts as executive of DOS as well."

text1= nlp(raw_text)

for word in text1.ents:
    print(word.text,word.label_)
    print(spacy.explain(word.label_))

