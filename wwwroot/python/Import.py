from Parser import Parser
import sys

def main(file,type,tableName):
    parser = Parser()
    if type == "excel":
        print(parser.parseExcelFile(file,tableName))
    elif type == "csv":
        print(parser.parseCSVFile(file,tableName))
    else:
        print("Invalid document type it should be CSV or XLS")
    
if __name__ == '__main__':
    if len(sys.argv) > 3:
        main(sys.argv[1], sys.argv[2], sys.argv[3])
    else:
        print("Not enought arguments provided")
