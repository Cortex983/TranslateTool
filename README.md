# TranslateTool

A simple _Computer Assisted Translation_ tool for translate plain text files,
with automatic recognize languages.

> For now, *Google Cloud Translate* are supported.

Usage
-----

In order to use the software, you can simply download a Visual Studio project from  [here](https://github.com/Cortex983/TranslateTool).

Once the application is running, just going to taype in to text box and after 3 sec your text will be translated. All Languages will be translated to Serbian. If you try translate Serbian or Croatian then result will be translated to English. 


> To know how to obtain the API key for the translation services, you can visit the sites from
 [Google Cloud Translate](https://cloud.google.com/translate/?hl=sr).
 
 
 Development
-----------

The software was developed using *C# MVC .NET* and *JavaScript*, so it requires *Google Cloud API*  for testing and compilation.

For running the software on development mode, you must: 

 1. Install dependencies with `Install-Package Google.Cloud.Translation.V2 -Version 1.1.0`.

 2. Create json file at Google Cloud Service to optain Private Kye. And Save data in to App_Data folder

3. At home controler change  code line with your new file name
 > string credential_path = HostingEnvironment.MapPath("~/App_Data/YOUR-FILENAME.json");
 
 
 
 License
-------

TranslateTool is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

TranslateTool is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
