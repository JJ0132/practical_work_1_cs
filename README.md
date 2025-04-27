# Practical Work 1 C# (Group 7)
This is the repository of the practical work 1 in C#. 

# INDEX

- [Introduction](#introduction)
- [Description](#landing-simulator-software-practical-work-i)
- [Problems](#problems)
- [Conclusions](#conclusions)

# Introduction
Group members:
   · Johan José Mérida Pérez
   · Javier Fuentes Guzmán
   · Alonso Fontecha Pérez


The propose of this practical work I is to develop a simulator of the aircrafts and the airport runaways to make them take off and land in the corresponding runaway based on the disponibility of them. All the work is based on C# on Object Oriented Programming and with methods such as polymorphism or inheritance. 

This project must contain a tick-based system and after every tick, the program must update in order to show new information about the Aircrafts flying at that moment and also the Airport Status showing if there is any free Runway for landing the aircrafts. 
Every Aircraft can be in 4 different states:
    · In Flight (when the program starts)
    · Waiting (when the aircraft has reached 0 km of distance and waiting for a free runway)
    · Landing (when the runway is free for landing)
    · On Ground (when the aircraft has already landed in a free runway)

There are 3 different types of aircrafts:
    · Commercial Aircraft (with number of passengers)
    · Cargo Aircraft (with maximum load)
    · Private Aircraft (with an Owner)

When running the program, it must execute a menu with a title welcoming the user to the Airport. After that, you have to choose if you want to insert a pre-made document with different aircrafts or select the second option which is making new aircrafts for the program. After that you can start your simulation manually of automatically with a tick-based system.


# Description 

The propose of this practical work I is to develop a simulator of the aircrafts and the airport runaways to make them take off and land in the corresponding runaway based on the disponibility of them. All the work is based on C# on Object Oriented Programming and with methods such as polymorphism or inheritance.
We decided to create a clear menu and easy understandable for the user.
The interaction of the user is really simple as it has to choose firstly if it wants to load a file that is already created or if it wants to create all the new aircrafts by its own. After that if he chooses using the simulation manually, it will print a text saying that for doing the different "ticks" in order to update the program you would have to press enter and it will change the different status of the aircrafts that are running in that moment. If the user choose the automatic simulation, every 2.5 seconds the program will be updated automatically, changing the status of the aircrafts and the runways of the Airport.

# Class Diagram

![Class Diagram]("C:\Users\Alonso\OneDrive - UFV\Documentos\POO\Class_Diagram.png")


# Problems 

At first we thought the practical work would be just doing some different methods, classes and subclasses for the Aircraft class but as we were doing it we realized it was not that easy and that we would have to stay a lot of hours developing the program, testing it, finding errors, fixing them... 

We faced some problems during the program construction as we had to revise the slides of the different units to revise all the theory.
Mainly, we had most of the problems in the Airport class as it is the largest one and the one that contains most of the methods of the program.
We had some problems trying to set which was the status of the aircrafts in the different moments of the simulation. To add, we faced some problems with the consumption of the fuel as the fuel capacity went directly to 0 after the first tick. 
To continue, our aircrafts, during all the program execution, stayed always in the "In Flight" state even if the distance was 0 but in the end we managed to solve it.


# Conclusions

To conclude, this project has made us realize that it is really complicated to develop a real full program although this, compared to other different fully developed programs it could be like just a "warm up". We understood how much time could a full team last to make a real program for different daily life situations.

With this practical work 1 we learned how to made the different relationships for the different subclasses as well as finding how to load a pre-made file with information of different aircrafts. 

As well, we started the project working everyone just by their own and we realized we did not communicate very well at the first moment but in the end, everytime we worked in the project we will make a group call in order to see what we had to change in the program to solve the different problems so we could continue doing the program. 



