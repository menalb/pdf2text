using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

var helpNeeded = Environment.GetCommandLineArgs().Contains("help");

var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
if (helpNeeded)
{
    Console.WriteLine("Parameters:");
    Console.WriteLine($"    pdf-filepath     input pdf file path ({processName} pdf-filepath=your-file.pdf)");
    Console.WriteLine($"    output-filepath  output text file path ({processName} output-filepath=your-output-file.txt)");
    Console.WriteLine();
    Console.WriteLine("Run");
    Console.WriteLine($"{processName} pdf-filepath=your-file.pdf output-filepath=your-output-file.txt");
    return;
}

var pdfFilePath = parseInputParameter(Environment.GetCommandLineArgs(), "pdf-filepath=");

if (string.IsNullOrEmpty(pdfFilePath))
{
    Console.WriteLine("Please provide a PDF input file (pdf-filepath=your-file.pdf)");
    Console.WriteLine($"Run '{processName} help' for more help");
    return;
}
if (!File.Exists(pdfFilePath))
{
    Console.WriteLine($"{pdfFilePath} does not exist!");
    return;
}

var outputFilePath = parseInputParameter(Environment.GetCommandLineArgs(), "output-filepath=");

if (string.IsNullOrEmpty(outputFilePath))
{
    outputFilePath = Path.GetFileName(pdfFilePath) + ".txt";
}

var sb = new StringBuilder();

Console.WriteLine($"Reading file {pdfFilePath} ...");

using (var pdf = PdfDocument.Open(pdfFilePath))
{
    sb.AppendLine(parseInformation(pdf.Information));

    sb.AppendLine("-----------------------------");
    sb.AppendLine();

    foreach (var page in pdf.GetPages())
    {

        var text = ContentOrderTextExtractor.GetText(page);

        sb.Append(text);
    }
}

Console.WriteLine($"Writing text output file {outputFilePath} ...");

System.IO.File.WriteAllText(outputFilePath, sb.ToString());

Console.WriteLine($"Text output file: {outputFilePath}");



string parseInputParameter(string[] args, string name)
{
    var par = args.FirstOrDefault(a => a.StartsWith(name));

    if (string.IsNullOrEmpty(par))
    {
        return string.Empty;
    }

    var tokens = par.Split("=");

    return tokens.Length > 1 ? tokens[1] : string.Empty;
}

string parseInformation(DocumentInformation info)
{
    var sbInfo = new StringBuilder();

    sbInfo.AppendLine($"{nameof(info.Author)}: {info.Author}");
    sbInfo.AppendLine($"{nameof(info.Creator)}: {info.Creator}");
    sbInfo.AppendLine($"{nameof(info.Title)}: {info.Title}");
    sbInfo.AppendLine($"{nameof(info.Producer)}: {info.Producer}");
    sbInfo.AppendLine($"{nameof(info.ModifiedDate)}: {info.ModifiedDate}");
    sbInfo.AppendLine($"{nameof(info.ModifiedDate)}: {info.ModifiedDate}");

    return sbInfo.ToString();
}
