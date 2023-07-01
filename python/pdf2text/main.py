import PyPDF2

pdffileobj = open('The Complete Works of H.P. Lovecraft.pdf', 'rb')

pdfreader = PyPDF2.PdfFileReader(pdffileobj)


print(pdfreader.documentInfo)

print(pdfreader.numPages)

pages = pdfreader.numPages


pageobj = pdfreader.getPage(4)

text = pageobj.extractText()

print(text)
result = ' '.join([i.replace(' ', '') for i in text])


out_file = open('out.txt', 'w')

out_file.writelines(result)
