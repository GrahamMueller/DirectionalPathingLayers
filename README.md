Linux build and test [![Build Net](https://github.com/GrahamMueller1992/DirectionalPathingLayers/actions/workflows/build.yml/badge.svg)](https://github.com/GrahamMueller1992/DirectionalPathingLayers/actions/workflows/build.yml)
Windows build and test [![Build Net](https://github.com/GrahamMueller1992/DirectionalPathingLayers/actions/workflows/build.yml/badge.svg)](https://github.com/GrahamMueller1992/DirectionalPathingLayers/actions/workflows/build.yml)

# DirectionalPathingLayers
Directional pathing layers for game I am making.

**Directional Vertices** Each vertex of the pathfinding grid stores if neighboring vertexes can be accessed, sort of like a 1 way street.  In some cases one vertex can path to another, but not in reverse.

**Layer Based** A scene is comprised of many objects, with each object containing a layer with pathing information around itself.  The pathfinding layer defines the default movement allowed, and individual objects store if they should keep default direction or !default direction. 

# Reason
To provide the base upon which pathfinding will be built on.   For organization I want to keep individual libraries in my game separate.  

This will prevent those little tiny hotfixes from slowly being added which can destroy these small modules. 

It also means that unit testing and benchmarking are much easier to accomplish. 

# What this is not
A useful library to be used elsewhere. While you may take this and use it yourself..why?  This is a kind of hyper focused data type.
