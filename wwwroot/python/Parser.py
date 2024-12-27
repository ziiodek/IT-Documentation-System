import pandas as pd
from Database import Database
import os

class Parser:
    def __init__(self):
        self.directory = os.getcwd()+"/wwwroot/Worksheets/"
        self.directory = self.directory.replace("python", "")

    def validateApplicationTemplate(self,data):
        if 'Name' in data.columns and 'LogonMethod' in data.columns and 'Owner' in data.columns and 'Tech' in data.columns and 'Notes' in data.columns:
            return True
        return False

    def ValidateServerTemplate(self,data):
        if 'Server Name' in data.columns and 'Version' in data.columns and 'IP Address' in data.columns and 'Patched By' in data.columns and 'Date Patched' in data.columns and 'Add Exclusions' in data.columns and 'Taegis Agent' in data.columns and 'DUO' in data.columns:
            return True
        return False

    def ValidateDowntimeTemplate(self,data):
        if 'Down or Degraded' in data.columns and 'Ticket No.' in data.columns and 'Start Time' in data.columns and 'End Time' in data.columns and 'Time Lapsed (HH:MM)' in data.columns and 'Time Lapsed in Minutes' in data.columns and 'Impact' in data.columns and 'Root Cause' in data.columns and 'Corrective Action' in data.columns and 'Comments if required' in data.columns and 'Date' in data.columns and 'Systems impacted' in data.columns and 'Updated By' in data.columns and 'Planned or Unplanned' in data.columns and 'Vendor Related/Internal' in data.columns:
            return True
        return False

    def ValidateSYMTemplate(self,data):
        if 'LPAR' in data.columns and 'Sym #' in data.columns and 'Disk' in data.columns and 'Size in MB' in data.columns and 'Version and SP' in data.columns and 'System Date'in data.columns and 'Creation Date\n' in data.columns and 'Owner' in data.columns and 'Point of Contact' in data.columns and 'Sym Functionality' in data.columns and 'Current Testing' in data.columns and "Active CWR's being tested" in data.columns and 'Devices Configured' in data.columns:
            return True
        return False

        
    def validateTemplate(self,tableName,data):
        match tableName:
            case "Downtime":
                return self.ValidateDowntimeTemplate(data)
 
            case "Server":
                return self.ValidateServerTemplate(data)
        
            case "SYMS":
                return self.ValidateSYMTemplate(data)

            case "Application":
                return self.validateApplicationTemplate(data)
                    
    def parseExcelFile(self,file,tableName):
        data = pd.read_excel(self.directory+file,dtype=str)
        
        if self.validateTemplate(tableName,data) == True:
            data = data.fillna(value="")
            database = Database()
            match tableName:
                case "Downtime":
                    database.InsertDownTimeRecords(data)
 
                case "Server":
                    database.InsertServersRecords(data)

                case "SYMS":
                    database.InsertSYMSRecords(data)

                case "Application":
                    database.InsertApplicationRecords(data)

            return "Template is valid"
        else:
            return "Invalid template"

    def parseCSVFile(self,file,tableName):
        data = pd.read_csv(self.directory+file,dtype=str)
        data = data.fillna(value="")
        database = Database()
        match tableName:
            case "Downtime":
                database.InsertDownTimeRecords(data)
 
            case "Server":
                database.InsertServersRecords(data)

            case "SYMS":
                database.InsertSYMSRecords(data)

            case "Application":
                database.InsertApplicationRecords(data)
       
        return ""

    def exportData(self,tableName):
        exportDir = os.getcwd()+"/wwwroot/Export/"
        database = Database()
        database.DatabaseConnection('ITDocumentation')
        df = pd.read_sql("""SELECT * FROM """+tableName+""";""", con=database.conn)
        df.to_csv(exportDir+tableName+'.csv',index = False)
