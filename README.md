# HS-Fulda-Excercise-GraphenUndNetzwerke
Excercises for the course 'Graphen und Netzwerke'

Das Repository enthält eine Bibliothek mit Graph-Algorithmen die in der Übung zu der Veranstaltung 'Graphen und Netzwerke' des Masterstudiengangs 'Angewandte Informatik' an der Hochschule Fulda implementiert wurden.
Die Bibliothek ist in der Solution 'GraphCollection' enthalten. Für die zugrundeliegende Datenstruktur der zu verwendenden Graphen wurde eine herkömmliche Objekt-/Klassenstruktur verwendet, bei welchem Graphen, Knoten und Kanten durch eigene Klassen repräsentiert werden. Die Bibliothek enthält u.a. folgende Graphalgorithmen:
- Breitensuche
- Topologisches Sortieren
- Dijkstra (kürzesete Wege)
- Kruskal (minimale Spannbäume)
- Prim (minimale Spannbäume)
- FordFulkerson (Maximaler Flussgraph)
- Floyd-Warshall (All pairs shorted path)
- StronglyConnectedComponents

Die Algorithmen werden über eine Konsolenanwendung bereitgestellt, deren Code in der Solution 'GraphApplication' zu finden ist. Die Anwenung erwaret grundsätzlich einen Dateinamen, welcher einen Graphen enthält, und den auszuführenden Graphalgorithmus samt Parameter als Argumente. Die Datei muss dabei im selben Ordner liegen wie die Anwendung. In dem Ordner GraphApplication/bin/Release/netcoreapp2.1 sind verschiedene Versionen für unterschiedliche Betriebssysteme zu finden. Die Versionen mit dem Prefix 'NET' sind 'standalone' Versionen und können ohne vorherige Installation des .NET Core 2.1.8 Frameworks verwendet werden. Dateien die einen Graphen enthalten finden sich unter GraphCollectionTest/TestFiles. Wird die Anwendung ohne Argumente aufgerufen erscheint ein Hilfe-Dialog, der die Verwendung noch einmal erläutert.
