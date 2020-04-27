import os
from pathlib import Path
import json
import sys

DOC_TEMPL="""<?xml version="1.0"?>
<doc>
    <assembly>
        <name>ASSEMBLY</name>
    </assembly>
    <members>
        MEMBERS
    </members>
</doc>"""
MEMBER_TYPE_TEMPL="""
<member name="T:MEMBER">
    <summary>
        SUMMARY
    </summary>
</member>
"""
MEMBER_METHOD_TEMPL="""
<member name="M:MEMBER">
    <summary>
        SUMMARY
    </summary>
    RETURNS
</member>
"""
MEMBER_RETURNS="""
    <returns>
        RETURNS
    </returns>
"""

members_l=[]

base_path = sys.argv[1]
assembly_name = sys.argv[2]

for root, dirs, files in os.walk(base_path):
    subpath = root.replace(base_path,"").strip(os.sep)
    components = subpath.split(os.sep)
    if len(components)<=1:
        continue
    components = [e[0].upper() + e[1:] for e in components]
    components = ".".join(components)
    for file in files:
        if ".json" in file:
            file_uc = file.replace(".json","")
            file_uc = file_uc[0].upper() + file_uc[1:]
            str_cont = Path(base_path+os.sep+subpath+os.sep+file).read_text()
            obj_cont = json.loads(str_cont)
            member_type=MEMBER_TYPE_TEMPL
            member_type=member_type.replace("SUMMARY",obj_cont["comment"])
            member_type=member_type.replace("MEMBER",components+"."+file_uc)
            members_l.append(member_type)
            for method_name,method_obj in obj_cont["methods"].items():
                if len(method_obj["parameters"])==0:
                    method_uc = method_name[0].upper() + method_name[1:]
                    member_method = MEMBER_METHOD_TEMPL
                    member_method=member_method.replace("MEMBER",components+"."+file_uc+"."+method_uc)
                    member_method=member_method.replace("SUMMARY",method_obj["comment"])
                    if "tags" in method_obj and "return" in method_obj["tags"]:
                        member_returns=MEMBER_RETURNS
                        member_returns=member_returns.replace("RETURNS",method_obj["tags"]["return"])
                        member_method=member_method.replace("RETURNS",member_returns)
                    else:
                        member_method=member_method.replace("RETURNS","")
                    members_l.append(member_method)

members="\n".join(members_l)
doc=DOC_TEMPL
doc=doc.replace("ASSEMBLY",assembly_name)
doc=doc.replace("MEMBERS",members)
f=open(assembly_name+".xml", "w")
f.write(doc)
f.close()


