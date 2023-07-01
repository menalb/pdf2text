import re

text = "b u t  o f  th is I  mus t  sa y  little ,  since  d e t a il e d  s"

#x = [i.replace(' ','') for i in re.findall(r"\w+\s\w+", text)]
x = text.split('  ')

print(' '.join([i.replace(' ','') for i in x]))