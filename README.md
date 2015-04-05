# Edifice
Edifice is an open source framework for creating building elements. The goals of the project are as follows:

- A core building element library which supports typical building elements like grids, levels, floors, walls, etc., with as flat an object hierarchy as possible whilst still being extensible.
- A set of interfaces which define an API for each layer of the the framework.
- An implementation of the database interop interface using a NoSQL database which can store elements from the core building element library as documents and unlimited amounts of meta-data associated with those elements.
- An element factory which manages the creation of building elements and their persistence in the database.
- A library of Dynamo wrapper types which allow the creation of building elements in Dynamo.
- A clean, well documented, and tested C# codebase.
- Mono-compatible.
