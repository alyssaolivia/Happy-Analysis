# Pursuit of Happiness

Happy_Analysis is an ASP.Net Core 3.1 MVC application used to predict customer happiness using several data models. 
The app was developed using Visual Studio 2019 Preview.


# Features
* Add a data point
* Add several data points from CSV file
* Upload several data models from CSV file
* Execute a single model
* Execute multiple models

## Site Map
- Home
	- Index
	- Privacy
- Model
	- Index
	- Create
	- Edit
	- Delete
	- Details
	- Execute
	- Execute All
	- Upload
- Data
	-  Index
	- Create
	- Edit
	- Delete
	- Details
	- Upload
- Run Analysis
	- Index
	- Delete
	- Details
- Privacy

# First-Time Run
Download the project and using Visual Studio, open Happy_Analysis.sln. There will be no database attached. 
Execute the app in Debug Mode.
Once running, go to https://localhost:44360/DataModel/Upload
* Download Models.csv to a folder of your choice.
* Select the **Choose File** button
* A file dialog will appear and select Models.csv where you saved it.
* Click **Upload File**
* If uploaded correctly, you will be redirected to https://localhost:44360/DataModel/Index

## Loading Data Points
Select a csv file which has the data in the format (Y,X1,X2,X3,X4,X5,X6) and the app assumes the files comes with these headings in the first row.
* Navigate to https://localhost:44360/DataPoint/Index and select **Upload Data File** button.
* Select the **Choose File** button
* A file dialog will appear and select your CSV dataset.
* Click **Upload File**
* If uploaded correctly, you will be redirected to https://localhost:44360/DataPoint/Index.

## Execution of Model(s)
* Navigate to https://localhost:44360/DataModel/Index
* In the list, each model has a link next that says *Run*. If clicked, the model is selected and you will be prompted to enter a reference point.
* Click **Run**
* You will be redirected to https://localhost:44360/ModelRun/Index on completion.

**OR**

* Navigate to https://localhost:44360/DataModel/Index
* Click **Execute All Models**
* All models will be selected and you will be prompted to enter a reference point.
* Click **Run**
* You will be redirected to https://localhost:44360/ModelRun/Index on completion.
