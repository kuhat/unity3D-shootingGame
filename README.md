# Game Specification
  ## 1.	Version of Unity: 2021.3.2f1c1 LTS
  ## 2.	Setups 
  ### 2.1	Tags and Layers
+ Go to Assets > Scenes > prefebs, there are several game objects that needs to be added with some tags.	
+ Find “SpeedUpBase” prefab, add tag “SpeedUpbase” and layer “Base” to it as shown in the figure below: 

![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/60e26cf7-c1c2-4a8b-ab25-4691edfda7e9)

<Center>Figure 1. “SpeedUpBase” prefab</Center>

+ Find “UnlimitedAmmoBase Varianat” prefab, add tag “AmmoBase” and layer “Base” to it as shown in the figure below: 

![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/1458ebf1-0225-4868-8a99-5b8f192b87c0)

<Center>Figure 2. “UnlimitedAmmoBase Variant” prefab</Center>

+ Find “AmmoPlusBase” prefab, add tag “AmmoPlusBase” and layer “Base” to it as shown in the figure below:
  
![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/d2e6cda8-3d20-4ad0-93c3-2d97fa9bd943)

<Center>Figure 3. “AmmoPlusBase Variant” prefab</Center>
Find “Tomb” prefab, add tag “Base” and layer “Base” to it as shown in the figure below: 

![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/951534f9-dd24-41f8-938f-cd03cd89f105)
<Center>Figure 4. “Tomb” prefab</Center>

+ Find “ZombieController” prefab, add tag “Enemy” to it as shown in the figure below: 

![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/6e80ee5b-f083-4289-b3a8-ffff964ee69e)

<Center>Figure 5. “ZombieController” prefab</Center>

+ Find “PlayerController” prefab, add tag “Player” to it as shown in the figure below:


![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/92e37afc-bf60-4314-ac26-c158fc29503f)

<Center>Figure 6. “PlayerController” prefab</Center>

Go to folder `Assets > Scenes > prefebs > PickUps`, there are several pickup game objects that needs to be added with some tags.

+ Find “SM_BoxAmmo_003 variant” and “SM_BoxAmmo_003 variant 1” prefab, add tag “Ammo” to it as shown in the figure below: 

![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/7de28000-d1ca-4ed3-92de-d1ed6da201d6)



<Center>Figure 7 “SM_BoxAmmo_003 variant” and “SM_BoxAmmo_003 variant 1” prefab</Center>

+ Find **“healthPickUp”** and **“healthPickUp1”** prefabs, add **“HealthPack”** tag to both of them.


![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/14494e04-854d-456e-963b-5cc66f4790f7)


<Center>Figure 8. “healthPickUp” and “healthPickUp1” prefab</Center>

+ Go to `Assets > Scenes > prefebs > MainScene`, find **“InitialBase1”, “InitialBase2”, “InitialBase3”, “InitialBase4”** under **Bases object**, add tag **“InitialBase”** and layer **“Base”** to them.



![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/759faa1c-6111-44b4-a6ae-967780d7ee98)





<Center>Figure 9. Initial bases in the main scene</Center>


### 2.2	Scene setting
AS it shows in figure 10, there are two scenes in this game. Firstly, drag MenuScene to the Build settings and then drag MainScene to the Build settings. 

![image](https://github.com/kuhat/unity3D-shootingGame/assets/53931269/62a89224-826c-4fac-b57f-17eb210b4527)


<Center>Figure 10. Build Setting for the scene</Center>

### 2.3	Game Starting
To start the game, Firstly, set up the display resolution to Full HD (1920 X 1080), double click Menu Scene in the Assets > Scenes. Then, click Play button, the game should perform properly.

