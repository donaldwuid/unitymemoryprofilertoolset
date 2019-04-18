import sys
import re
import os

__author__ = "donaldwu"

def SizeStringToNumKB(sizeString):
    strSizeString = str(sizeString).lower()
    strSizeString = re.sub(r"\W", "", strSizeString)
    # print(strSizeString)
    searched = re.search(r'\D', strSizeString)
    if searched and searched.start() > 0:
        floatSize = float(strSizeString[0: searched.start()])
        unit = strSizeString[searched.start()]
        print(unit)
        if unit == "k":
            return floatSize
        elif unit == "m":
            return floatSize * 1024.0
        elif unit == "g":
            return floatSize * 1024.0 * 1024.0
    else:
        return float(strSizeString) / 1024.0

        
    # print(strSizeString)
    # lastDigit = re.finditer(r"\D", strSizeString)
    # print(strSizeString, lastDigit)
    #if str(sizeString).lower.endswith("k") or str(sizeString).lower.endswith("kb"):



class SortItem:
    def __init__(self, rawData:str, sizeColumns):
        self.rawData = rawData
        self.totalSize = 0.0

        searched = re.search(r'\[.*\]', rawData)
        if searched:
            sizeSegment = searched.group(0)
            print(sizeSegment)
            splitted = sizeSegment.split()
            for oneColumn in sizeColumns:
                oneColumnIndex = int(oneColumn)
                oneColumnContent = splitted[oneColumnIndex]
                oneColumnValue = SizeStringToNumKB(oneColumnContent)
                #print(oneColumnIndex, oneColumnContent, oneColumnValue)
                self.totalSize += oneColumnValue

    def __lt__(self, other):
        return self.totalSize < other.totalSize

    def __str__(self):
        return self.rawData
        

def sort_vmmap_verbose(filepath, sizeColumns):
    print(filepath + str(sizeColumns))
    f = open(filepath)
    list = []
    for oneLine in f:
        newItem = SortItem(oneLine, sizeColumns)
        list.append(newItem)
    
    list.sort(reverse=True)

    
    splittedPath = os.path.splitext(filepath)
    newFilePath = splittedPath[0] + "_sorted" + splittedPath[1]

    newFile = open(newFilePath, "w")
    for oneItem in list:
        newFile.write(str(oneItem))

    newFile.close()

    print("sorted and write to file: " + newFilePath)

def main():
    sort_vmmap_verbose(sys.argv[1], sys.argv[2:])
        

if __name__== "__main__":
  main()


