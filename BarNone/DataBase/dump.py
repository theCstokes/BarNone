#!python

import subprocess
import os

user = "postgres"
pw = "admin"
host = "127.0.0.1"
db="BarNone"
 
dir_name = os.getcwd()
dump = """ "c:\\program files\\postgresql\\10\\bin\\pg_dump" -U %s -Z 9 -f "%s" -F c %s  """                  
os.putenv('PGPASSWORD', pw)
 
file_name = db  + ".backup"
command = dump % (user,  dir_name +"\\"+ file_name, db)
print(command)
subprocess.call(command,shell = True)
print("%s Dump finished" % db)

