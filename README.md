# FitbitJsonConverter
Converts the Fitbit json Archive data format to the CSV format that Garmin and other products expect for importing data. Allows the user to import all of their timeline data instead of just the last month.

As of this writing, the FitBit export will only export the last 31 days in a CSV format that Garmin will take as an import format. However they will export your entire data history in a json format. This json format is not importable into Garmin's connect website. This program will take the json format and create a CSV file that you can then import all of your history into the Garmin Connect. 

Currently this program will only convert Steps, Weight, and Sleep. Garmin will only actually import the weight and steps (activities), it won't take in the sleep data. This program will create multiple csv files as there has been some difficulty with importing the single, large csv file.

This code is copyright 2021 Sunbreak Software, LLC

You may use this program for your own conversion, however use at your own risk of course. It worked great for my account, but I can't take all of the differences in your data into consideration. If you hit an error when using the program, feel free to email me and share your data with me and I can attempt to fix the bug. In addition, if you'd like more of your data (I didn't use things like food, etc), let me know and I can easily modify the program.

How to use:

* Download the https://github.com/wadebaird/FitbitJsonConverter/blob/master/Sunbreak.FitbitJsonConverter%20Setup.zip
* Unzip to a folder
* Run setup.exe (this program is not "signed" so it will say it's from an unknown publisher, accept if you like)
* This should install the .Net runtime as well as the program and create a shortcut on your desktop.
* Run the program and output the files on your machine.
* The output should be in multiple .CSV files and should be importable to Garmin Connect. The sleep data won't seem to import for me, and I get an error when attempting it. All of my research online says Garmin won't take in the sleep data. So I just have only tried the Body and Activities files.
* Enjoy and let me know if there are any problems / questions

wadeb@sunbreaksoftware.com
