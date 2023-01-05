Create a table in SQL Server by running the sql-cache create command. Provide the SQL Server instance (Data Source), database (Initial Catalog), schema (for example, dbo), and table name (for example, TestCache):

```
dotnet sql-cache create "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DistCache;Integrated Security=True;" dbo TestCache
```

A message is logged to indicate that the tool was successful:

```
Table and index were created successfully.
```

# Benchmark result
Both Setting and Getting value with IDistributedCache.
```
wrk -t 12 -c 250 -d 10s
```


<table>
  <tr>
  <th ></th>
    <th colspan="2">Redis</th>
    <th colspan="2">Sql</th>
    <th colspan="2">Memory</th>
  </tr>
  <tr>
    <th></th>
    <th>string</th>
    <th>byte[]</th>
    <th>string</th>
    <th>byte[]</th>
    <th>string</th>
    <th>byte[]</th>
  </tr>
  <tr>
    <th>Requests/sec</th>
    <th>15,619.25</th>
    <th>13,784.19</th>
    <th>3,274.93</th>
    <th>3,140.92</th>
    <th>19,353.72</th>
    <th>13,368.91</th>
  </tr>
  <tr>
    <th>Transfer/sec</th>
    <th>39.55MB</th>
    <th>34.88MB</th>
    <th>8.29MB</th>
    <th>7.94MB</th>
    <th>48.66MB</th>
    <th>33.73MB</th>
  </tr>
</table>

