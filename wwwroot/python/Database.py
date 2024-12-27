import pyodbc
from datetime import datetime
import ctypes 


class Database:

    def __init__(self):
        self.server = 'v00483048'
        self.username = 'ITDoc'
        self.password = "1td0cum3nt@t10n@2023"
        self.conn = None

    def DatabaseConnection(self,databaseName):
        self.database = databaseName
        #ODBC DRIVER VERSION FOR TEST SERVER AND LIVE SERVER
        #self.connectionString = f'DRIVER={{ODBC Driver 13 for SQL Server}};SERVER={self.server};DATABASE={databaseName};UID={self.username};PWD={self.password};TrustServerCertificate=yes;Trusted_Connection=no'
        #ODBC DRIVER VERSION OF YOUR LOCAL COMPUTER
        self.connectionString = f'DRIVER={{ODBC Driver 18 for SQL Server}};SERVER={self.server};DATABASE={databaseName};UID={self.username};PWD={self.password};TrustServerCertificate=yes;Trusted_Connection=no'
        
        self.conn = pyodbc.connect(self.connectionString)

    def InsertApplicationRecords(self,dataframe):
        query = """INSERT INTO Application (
                                         Name,
                                         LogonMethod,
                                         UsersFrom,
                                         Owner,
                                         Tech,
                                         Notes,
                                         AuthorName,
                                         DateTime
                                    ) VALUES(?,?,?,?,?,?,?,?);"""

        author = 'System'
        dateTime = datetime.now()
        self.DatabaseConnection('ITDocumentation')
        cursor = self.conn.cursor()
        for index, row in dataframe.iterrows():
            cursor.execute(query,(
                            row['Name'],
                            row['LogonMethod'],
                            row['UsersFrom'],
                            row['Owner'],
                            row['Tech'],
                            row['Notes'],
                            author,
                            dateTime
                            ))
        self.conn.commit()
        cursor.close()
        self.conn.close()

    def InsertServersRecords(self,dataframe):
        query = """INSERT INTO Server (
                                         Name,
                                         Version,
                                         Ip,
                                         Status,
                                         PatchedBy,
                                         DatePatched,
                                         AddExclusions,
                                         TaegisAgent,
                                         DUO,
                                         AuthorName,
                                         DateTime,
                                         Notes
                                    ) VALUES(?,?,?,?,?,?,?,?,?,?,?,?);"""

        author = 'System'
        dateTime = datetime.now()
        status = 'Online'
        self.DatabaseConnection('ITDocumentation')
        cursor = self.conn.cursor()
        for index, row in dataframe.iterrows():
            #note = ""
            note = str(row['Unnamed: 8'])+" "+str(row['Unnamed: 9'])+" "+str(row['Unnamed: 10'])
            cursor.execute(query,(
                            row['Server Name'],
                            row['Version'],
                            row['IP Address'],
                            status,
                            row['Patched By'],
                            row['Date Patched'],
                            row['Add Exclusions'],
                            row['Taegis Agent'],
                            row['DUO'],
                            author,
                            dateTime,
                            note
                            ))
        self.conn.commit()
        cursor.close()
        self.conn.close() 

    def InsertDownTimeRecords(self,dataframe):
        query = """INSERT INTO Downtime (
                                         Status,
                                         Ticket,
                                         StartTime,
                                         EndTime,
                                         TimeLapsed,
                                         TimeLapsedMinutes,
                                         Impact,
                                         Cause,
                                         CorrectiveAction,
                                         Notes,
                                         AuthorName,
                                         DateTime,
                                         Date,
                                         SystemImpacted,
                                         Requestor,
                                         EventType,
                                         Owner
                                    ) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);"""

        author = 'System'
        dateTime = datetime.now()
        #(dataframe.columns.tolist())
        self.DatabaseConnection('ITDocumentation')
        cursor = self.conn.cursor()
        for index, row in dataframe.iterrows():
            cursor.execute(query,(
                            row['Down or Degraded'],
                            row['Ticket No.'],
                            row['Start Time'],
                            row['End Time'],
                            row['Time Lapsed (HH:MM)'],
                            row['Time Lapsed in Minutes'],
                            row['Impact'],
                            row['Root Cause'],
                            row['Corrective Action'],
                            row['Comments if required'],
                            author,
                            dateTime,
                            row['Date'],
                            row['Systems impacted'],
                            row['Updated By'],
                            row['Planned or Unplanned'],
                            row['Vendor Related/Internal']
                            ))
        self.conn.commit()
        cursor.close()
        self.conn.close()

        return ctypes.windll.user32.MessageBoxW(0, "Data Uploaded", "Status", 1)
        

    def InsertSYMSRecords(self,dataframe):
        query = """INSERT INTO SYMS (
                                         LPAR,
                                         SymNo,
                                         DiskQuantity,
                                         SizeMB,
                                         VersionSP,
                                         SystemDate,
                                         SYMCreationDate,
                                         Status,
                                         Owner,
                                         PointContact,
                                         SymFunctionality,
                                         CurrentTesting,
                                         ActiveCWRTested,
                                         AuthorName,
                                         DateTime,
                                         DevicesConfigured                                   
                                    ) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);"""

        status = 'Active'
        author = 'System'
        dateTime = datetime.now()
        #print(dataframe['Creation Date'])
        self.DatabaseConnection('ITDocumentation')
        cursor = self.conn.cursor()
        for index, row in dataframe.iterrows():
            cursor.execute(query,(
                            row['LPAR'],
                            row['Sym #'],
                            row['Disk'],
                            row['Size in MB'],
                            row['Version and SP'],
                            row['System Date'],
                            row['Creation Date\n'],
                            status,
                            row['Owner'],
                            row['Point of Contact'],
                            row['Sym Functionality'],
                            row['Current Testing'],
                            row["Active CWR's being tested"],
                            author,
                            dateTime,
                            row['Devices Configured']
                            ))
        self.conn.commit()
        cursor.close()
        self.conn.close()
        
    
        