# Databases



## General
In this directory all required databases are stored.



## Geodata

The original geodata are data from [OpenStreetMap](https://www.openstreetmap.org/) and downloaded from
[GEOFABRIK](http://download.geofabrik.de/) as BZ2 file. 
In the BZ2 file there is a OSM file. OSM data are based on XML an can be parsed with
any XML Parser. 

- The format is described here: [Elements](https://wiki.openstreetmap.org/wiki/Elements)

- The content and meaning are documented here: [Map Features](https://wiki.openstreetmap.org/wiki/Map_Features)

The OSM File is parsed with a (currently private) tool and only a set of important nodes are stored in the SQLite Database(s).
The idea is to have a small set of geodata for 
- Query, locating and display towns, cities or villages
- Query, locating and display man made objects



## Generated GeoDatabases

- freiburg-regbez-latest.osm.sqlite

    Is the SQLite Database from : [freiburg-regbez-latest.osm.bz2](http://download.geofabrik.de/europe/germany/baden-wuerttemberg/freiburg-regbez-latest.osm.bz2)


- tuebingen-regbez-latest.osm.sqlite

    Is the SQLite Database from : [tuebingen-regbez-latest.osm.bz2](http://download.geofabrik.de/europe/germany/baden-wuerttemberg/tuebingen-regbez-latest.osm.bz2)