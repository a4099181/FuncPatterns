# Func < Patterns >
######_sample chain of responsibility_ **ShapeReader**

MonadicReader realize factory method design pattern and builds a chain that read sample xml data.

All other classes compose complex ShapeReader functionality.
Every single class is responsible for recognizing exact XML element and should read it when it is possible.

That is the way to achieve:
- open-close design principle, because when new XML element will come, then there is no need to change any class here. New class for new XML element should be created.
- single responsibility principle, because there is the only one reason to change every single class here. If format of an XML element is changed, then apropriate class should be changed also to handle new XML document format.
