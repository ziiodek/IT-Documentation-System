from Parser import Parser
import sys

def main(tableName):
    parser = Parser()
    return parser.exportData(tableName)
    
if __name__ == '__main__':
    if len(sys.argv) > 1:
        main(sys.argv[1])
    else:
        print("Not enought arguments provided")