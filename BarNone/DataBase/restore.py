#!python

import subprocess
import os

user = "postgres"
pw = "admin"
host = "127.0.0.1"
port = "5433";
db="BarNone"

dir_name = os.getcwd()
restore = """ "C:\\Program Files\\PostgreSQL\\10\\bin\\pg_restore" -h %s -p %s --clean -U %s -d %s "%s" """   #Change number based on whatever version you are using                
os.putenv('PGPASSWORD', pw)

file_name = db + ".backup"   
command = restore % (host, port, user, db, dir_name + "\\"+ file_name)
print(command)
subprocess.call(command,shell = True)
print("%s Restore finished" % db)

