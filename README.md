# ObeidatLog
C#.Net very fast text, XML files, or SQL database log for web applications.
I needed to record every request in my web application and after 3 months of searching and practicing for the best way to log a record without affecting the time of web request and performance.

### The main idea in this log is the sacrifice:
1. **Instantaneous:** What I mean is that this log does not write the request instantaneously; it may be delayed for a maximum of 30 seconds, so as to ease the pressure on the IO.
2. **Ordering:** This is because the requests written in the log are written with the time and thus there is no need to arrange which expedits the writing of the requests once.

### Log types:
1. text file.
2. Database tabels.

### Its advantages are as follows:
- It can carry a very large number of concarent requests; by stored list of requests in a series with a capacity an item and write it once upon completion of the arrival to the limit or after 30 seconds.
- Replace the IO lock by using [Blocking Collection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.blockingcollection-1?view=netframework-4.7.2), which ensures the speed and it is [thread-safe](https://msdn.microsoft.com/en-us/library/a8544e2s.aspx).
- All actions in the log run on different thread and therefore do not affect the speed or performance of the system.
- It has windows application tool for read, sort and filter items in the log.
- Replace the check-conditions when choosing the level or type by using [Template design pattern](https://en.wikipedia.org/wiki/Template_method_pattern) so reduce the time of check the level or type in every write log item.
- When selecting a file (text or XML), the log creates a file for each day and names it with system name and the date of the day for each level of the record. When selecting a database, the log creates a table for each level (if not exists) with system name and the level.
- The record contains 4 different levels:
  - None.
  - Exceptions only.
  - Exceptions and Information.
  - Exceptions, Information and debug.
- The log automatically excludes base64 and any properties added in configuration.
- In exception the log write with the exception message:
  - File name. 
  - Method name.
  - Line number.
  - Caption (optional).
- The exception message come from every inner-exceptions(if exests) for this exception.
- The debug wite with every request body:
  - IP address.
  - Device name. 
  - HTTP method.
  - Username, 
  - Client name.
  - Url for this request.
  
  ### Configuration
  #### all configuration come from appSettings tag in Web.config below:
  -  **LogLevel:**  level you will use log for
     - 0 => No log.
     - 1 => Exception only.
     - 2 => Exception and Info.
     - 3 => Exception, Info and Debug.
  -  **LogSource:** log writting source
     - 0 => Text file.
     - 1 => Sql Database.
  -  **LogName:** the name of project
     - string value.
  -  **LogCapacity:** how many items will hold in buffer until write them to the source.
     - integer number value.
  -  **AdditinalHour:** if there is number of hours need to be added or subtract from hosting site server time.
     - integer number value.
  -  LogPath:** directory for log (without file name or extension) to be write in if the log source is text file.
     - string value.
  -  **EscapeParameters:** if there is sensitive parameter which you wont to log them like password or somthing.
     - string value ( for multible valuse just seperate them with ",").
  -  **LogConnection:** connection string if source is Sql database.
     - string value.
  
