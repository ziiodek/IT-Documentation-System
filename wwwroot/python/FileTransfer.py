import shutil
import os

def main():
	root_directory = os.getcwd()+"/wwwroot/DocumentsUploaded/"
	target_directory = "R:\ITDocumentation_website\DocumentsUploaded"
	for root,dirs,files in os.walk(directory):
		for file in files:
			shutil.copyfile(file,target_directory)

if __name__=="__main__": 
    main() 
			
	