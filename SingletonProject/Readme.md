# File handler singleton
In the real world I have used singleton for file handling and database handling. This project is an example for the file handling.
In the future I will use this solution for logging.

## Plan
Only one public method will be available for the callers that needs a string parameter: public void Print(string text)
In this method we will add the given text to a threadsafe queue and then if the printing task is not run we start it.
The printing task dequeueing the elemnts from the queue and write them into the file.
Thats all.
