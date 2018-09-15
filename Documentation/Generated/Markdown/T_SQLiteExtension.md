# Class SQLiteExtension
## Base Class
- Object
## Methods
Flags|Result|Name|Parameters
-|-|-|-
*static*|Void|Analyze|( SQLiteConnection dbConnection )
*static*|Void|Reindex|( SQLiteConnection dbConnection )
*static*|Void|Vacuum|( SQLiteConnection dbConnection )
*static*|Void|DropTable|( SQLiteConnection dbConnection , String strTablename )
*static*|Boolean|TableExists|( SQLiteConnection dbConnection , String strTablename )
*static*|List&lt;T&gt;|GetTableNames|( SQLiteConnection dbConnection )
*static*|List&lt;T&gt;|GetViewNames|( SQLiteConnection dbConnection )
*static*|Void|Truncate|( SQLiteConnection dbConnection , String strTablename )
*static*|Int64|GetLastPrimarykey|( SQLiteConnection dbConnection )
*static*|SQLiteCommand|PrepareInsertStatement|( SQLiteConnection dbConnection , String strTablename , Boolean bIgnorePrimaryKey )

<br /><hr />
SIGENCEScenarioTool.Library, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
