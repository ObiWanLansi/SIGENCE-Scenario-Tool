# Class IDbConnectionExtension
## Base Class
- Object
## Methods
Flags|Result|Name|Parameters
-|-|-|-
*static*|IEnumerable&lt;T&gt;|Select|( IDbConnection dbConnection , String strFormat , Object[] args )
*static*|IEnumerable&lt;T&gt;|Select|( IDbConnection dbConnection , String strSelectStatement )
*static*|Int32|ExecuteNonQuery|( IDbConnection dbConnection , String strFormat , Object[] args )
*static*|Int32|ExecuteNonQuery|( IDbConnection dbConnection , Int32 iTimeout , Boolean bTransaction , String strFormat , Object[] args )
*static*|Object|ExecuteScalar|( IDbConnection dbConnection , String strFormat , Object[] args )
*static*|Object|ExecuteScalar|( IDbConnection dbConnection , Int32 iTimeOut , String strFormat , Object[] args )
*static*|SortedDictionary&lt;T1,T2&gt;|GetSortedDictionary|( IDbConnection dbConnection , String strSelectStatement )
*static*|Dictionary&lt;T1,T2&gt;|GetDictionary|( IDbConnection dbConnection , String strSelectStatement )
*static*|Boolean|CloseIfOpen|( IDbConnection dbConnection , Boolean bIgnoreCloseException )
*static*|Void|SaveAsCSV|( IDbConnection dbConnection , String strSelectStatement , FileInfo fiExportFile , Char cDivider )
*static*|DataTable|SelectAsDataTable|( IDbConnection dbConnection , String strResultTableName , String strFormat , Object[] args )

<br /><hr />
SIGENCEScenarioTool.Library, Version=15.0.0.0, Culture=neutral, PublicKeyToken=null
