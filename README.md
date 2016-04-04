C# Bootstrap/AngularJS View Generator Collections

I was tired of having to write custom views for each object I would create and I did not like the way that the view generation scaffolding functionality created views in an asp.net mvc project.  I would have to go in every time
and clean up the generated views so that they looked cleaner and if the object had a lot of different properties this was a time consuming process each time.  A lot of razor html helpers become emedded in the code and I am
 personally not a fan of any of them I like my views to be as much clean html as possible with little to no server side rendering. So on a lazy sunday I decided to start creating a program that could generate clean views
 for me.
 
 The purpose of this project is to take POCO C# objects and generate clean bootstrap html pages(either full or partial) automagically from the object and it's various properties.  AngularJS view (and eventually controller)
 generation functionality will be included as well for those like myself who always like to have an angular controller as a backend for every view.

Propeties must be written in camel case for clean view generation for now.  Only string, Datetime, and int types are supported.  Support for complex nested objects will come later on down the road.

Refer to the main thread in BootstrapGenerator.cs for an example.