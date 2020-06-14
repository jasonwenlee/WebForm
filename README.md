# Conquest WebForm
Web form for Conquest App. This is a simple MVC web application that allows a user to create a request and attach a document to this same request. This webform is designed based on the **[unit test](https://github.com/jasonwenlee/ConquestTests)** performed earlier in the assessment.

## Layout
1. This is an MVC web form with three pages.
2. First page consists of three input fields which are Requestor Name, Requestor Detail, and Organisation Unit (displayed as a drop down list with org. name)
3. Second page is displayed after creating a successful request. This page displays the request ID, a doc. description field, and a file upload input.
4. Third page is displayed after a file is successfully uploaded.

## Features
Main page requires all fields to be entered before proceeding just like in the native mobile application.
Web Form | Native mobile application
--------------------- | ------------------
<img src="https://imgur.com/0zqlfxp.png" height=250 width=350> | <img src="https://imgur.com/aWF54xI.png" height=570 width=270> 

Upon creating a request, it redirects the user to the next page for file upload. It also shows the Request ID and the success message indicating a request has been submitted. This section requires a document description when uploading a document similar to the mobile application.
Web Form | Native mobile application
--------------------- | ------------------
<img src="https://imgur.com/yBGql5T.png" width=347 height=399> | <img src="https://imgur.com/6wwXIwY.png" height=570 width=270>

After uploading a file, it redirects the user to the final page.

<img src="https://imgur.com/U7wvy26.png" width=30% height=30%>
