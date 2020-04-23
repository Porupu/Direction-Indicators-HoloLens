# Direction-Indicators-HoloLens
The main folder conatins the Unity (2019.2.6f1) project.  
The *App* folders are Unity builds of the main scene and they contain the Visual Studio (2017) solution that can be opened in HoloLens Emulator.  
Emulator version used: 10.0.17763.134  
HoloLens Emulator system requirements:  
The HoloLens Emulator uses Hyper-V with RemoteFx (1st Gen Emulator) for hardware accelerated graphics.  
64-bit Windows 10 Pro, Enterprise, or Education (Windows 10 Home Edition does not support Hyper-V or the HoloLens Emulator.)  
64-bit CPU with 4 cores (or multiple CPUs with a total of 4 cores)  
8 GB of RAM or more  
In the BIOS, the following features must be supported and enabled:  
Hardware-assisted virtualization  
Second Level Address Translation (SLAT)  
Hardware-based Data Execution Prevention (DEP)  
GPU requirements:  
DirectX 11.0 or later  
WDDM 1.2 graphics driver or later  
The emulator might work with an unsupported GPU, but will be significantly slower  
  
Running the *App* in the Emulator should display a rather large sphere in front of the user, with appropriate arrow indicators using its 
Vector3 location as the reference to change rotation to be pointing in its direction. After a brief delay, new sphere spawn in a random 
location around the user. Each sphere disappears after a short time. New spheres are then continously spawned in a set timer.  
  
The *App - multiple sources* contatins 13 spheres in the first scene, all of which are rendered when the application starts but disappear
shortly. New random spheres then continue to spawn as in the first example.  
  
Lastly, the *App - smaller spheres* should behave in the same way as *App*, however, spheres are scaled down to a 0.25 of their size, 
making them less obtrusive.

It is possible to use mouse inputs in the Emulator to turn the camera (simulating turning of the user's head). Indicators will adjust their
relative rotation to be constantly pointing to their associated spheres. Spheres can come into view for visual confirmation.  
  
This project is a part of University of South Wales final year Individual project - Computer Games Development dissertation.
