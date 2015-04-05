# Edifice
Edifice is an open source framework for creating building elements. The goals of the project are as follows:

- A core building element library which supports typical building elements like grids, levels, floors, walls, etc., with as flat an object hierarchy as possible whilst still being extensible.
- A set of interfaces which define an API for each layer of the the framework.
- An implementation of the database interop interface using a NoSQL database which can store elements from the core building element library as documents and unlimited amounts of meta-data associated with those elements.
- A library of Dynamo wrapper objects which handle persisting core building elements in the database.
- A clean, well documented, and tested codebase.
- Mono-compatible.
