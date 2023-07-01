import pdfplumber
import sys


pdf_filepath = sys.argv[1]
out_filepath = sys.argv[2]

parameters = dict(filter(lambda c: len(c) == 2, [par.split('=') for par in sys.argv]))

pdf_filepath = parameters['pdf-filepath']
out_filepath = parameters['output-filepath']

with pdfplumber.open(pdf_filepath) as pdf:
    # Print PDF Metadata
    for k in pdf.metadata.keys():
        print(f'{k} : {pdf.metadata[k]}')

    # extract pages
    pages = len(pdf.pages)
    print(f'Pages: {pages}')
    out_file = open(out_filepath, 'w', encoding="utf-8")

    for p in pdf.pages:
        page_message = f'page {p.page_number} of {pages}'
        print(page_message)
        out_file.writelines(p.extract_text() + ' \n' +
                            page_message.upper() + ' \n')
