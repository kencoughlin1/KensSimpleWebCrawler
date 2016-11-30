This is a console app that requires .Net 4.5.2 installed on a windows 7 or greater.
An executable is included with this project, included in a zip file KensSimpleWebCrawler.zip.
Extract to a windows folder and execute the console app with a URL for example: KensSimpleWebCrawler.exe http://wiprodigital.com/

Trade-offs (to keep in the suggested time scales).
This is a simple app that really only looks a fully qualified href's.
I am returning Links/Images/Metadata
I haven’t looked at Css attributes.
Simple recursive application rather than using a better algorithm like Depth First Search.
Don’t take in/adhere to Robot.txt
