# **Documentation and things from other sripts**
  
## 1. How to access a _variable_ from **ScriptA** to **ScriptB**:  

* In **ScriptA** declare a _static_ variable:

```C#
static float varToAccess;
```

* In **ScriptB** you have to reference the object that contains the **SriptA**:

```C#
public GameObject objectA; // Drag 'n' drop from the inspector

Debug.Log(objectA.GetComponent<ScriptA>.varToAccess); // It'll show in console the value of varToAccess
```

#

## 2. How to find a **_GameObject_** by its `tag`

 First you need to set a tag for your GameObject, in this example it will be **Player**. Then declare a GameObject and set it to the one that has teh tag **Player**:

```C#
private GameObject _player; // We declare a GameObject

void private void Start()
{
    _player = GameObject.FindWithTag("Player"); // Reference of the G.O. and its tag 
}
```
#

## 3. How to get a button from the **_UI_** to work

* Define a public method:
```C#
public void StartClicked()
{
    SwitchState(State.INIT); // Example from the GameManager code
}
```
* Then create a button in Unity and in its configuration go to _event_ --> _on click_, then choose _Game Manager_ and the method **_StartClicked()_**.

#

## 4. How to use **_Getters_** and **_Setters_**

> This is one of the C# properties
* First we create a public variable:

  ```C#
    public int coins;
  ```

* Then we make use of the C# property:

```C#
public int Coins
{
    get { return coins; }
    set { coins = value; } // We can add more things to the set part
}    
```

* Then we declare a variable from we can access to:

```C#
public static GameManager Instance { get; private set; } // Doing it just once is enough for other variables
```

* Then in the **_Start_** method write:

```C#
Instance = this; 
```

* Now we can access the variable **_Coins_** from other script:

```C#
// In other script
if(something_happens)
{
    GameManager.Instance.Coins ++; // In this case we add one coin to the external variable.
}
```

#

## 5. How to know how many **_GameObjects_** with the same **_tag_** are in the scene

* First we have to create an **_array GameObject_** to contain all the prefabs:

```C#
private GameObject[] coinsInLevel; 
```

* Then we initialize the variable as the prefab we want to count:

```C#
void Start()
{
    coinsInLevel = GameObject.FindWithTag("Coin"); // The array will store every GameObject with that tag
}
```

* Finally, to get the amount of **_GameObjects_** in the scene, just write:

```C#
Debug.Log(coinsInLevel.Length); // Each position of the array is a GameObject contained
```

#

## 6. The _Invoke_ method

* This method can only be called if it returns **void** and it has no **parameters**.

```C#
void Update()
{
    Invoke (Method_Name, Float_Delay); // Example
    Invoke (StartClicked(), 0.5f);   
}
```

#

## 7. How to **_create_** and **_destroy_** clones of prefabs

* First we need a reference to the **_GameObject_** we want to instantiate:

```C#
public GameObject _player; // Drag 'n' drop the object to the inspector
```

* Then we create an empty GameObject that it'll instantiate **__player_**:

```C#
private GameObject instancePlayer; 
```

* now we can instantiate **__player_** with the empty _GameObject_:

```C#
void Start()
{
    instancePlayer = Instantiate(_player); // It instantiates a clone    
}
```

* Now we can destroy it like so:

```C#
Destroy(instancePlayer); // It destroys the clone of the prefab
```

#

## 8. How to use the **_UI_** and **_Scene_** functions

* Just add the libraries at the top.
>`using UnityEngine.UI;` 

>`using.UnityEngine.SceneManager;`  

#

## 9. How to use a **_State machine_**

#

## 10. How to use a **_Coroutine_**

#

## 11. How to **_follow_** a GameObject (for cameras):

In order to make the camera to follow a **_GameObject_** we have to set it the same position as the **_GameObject_**, but we have to add an **_`offset`_** to be able to see the **_GameObject_** we want to follow.

* First we declare the variable for the offset we want:

```C#
    private Vector3 offset = new Vector3 (0, 1, -10); // new Vector3 (x, y, z);
```

* Then in the **_Update_** method we set its value:

```C#
void Update()
{
    this.transform.position = GameObject.FindWithTag("Object_To_Follow").transform.position + offset;
}
```

#

## 12. Making the player jump

* First we need to add a **_Rigidbody_** component to our `player` in the inspector. Then we need to reference that **_Rigidbody_** in our code:

```C#
public Rigidbody rb;
```

* In the method ``Start``() we initialize the variable:

```C#
rb = this.GetComponent<Rigidbody>(); // "this" references the GameObject itself
```

* Then we add a force (`VelocityChange`) upwards to the rigidbody:

```C#
void FixedUpdate()
{
    rb.AddForce(Vector3.up * float_variable, ForceMode.VelocityChange);
}
```

#

## 13. The horizontal movement of the player

* We declare a float variable that will contain a number if a button is pressed:

```C#
private float horizontalInput; // If the input device has sensitivity functions, the float variable comes in handy
```

In Unity there's something called **`Input Manager`** where you can see and configure the controls for your game. In this case we're going to use the **_Horizontal_** axes, which is already configured to the keyboard buttons `A`, `B` or the `left` and `right arrows`. You can access there by going to:

>Edit --> Project Settings --> Input Manager

* Then in the **_Update_** we initialize the variable:

```C#
Update()
{
    horizontalInput = Input.GetAxis("Horizontal") * 4;
}
```
* The **_horizontalInput_** variable will get from the `Horizontal` axis either **-1** for left or **1** for right. Or `< 0 for left` (less than zero) or `> 0 for right` (greater than zero).

* Now to make the player move we need to use its `Rigidbody`:

```C#
void FixedUpdate()
{
    rb.velocity = new Vector3(horizontalInput, rb.velocity.y, 0); // (x, y, z)   
}
```

The **`x`** axis has the horizontal movement according to `left` (**-1**) and `right` (**1**). The **`y`** axis is the **_Rigidbody_** own velocity, if we put `0` instead, the player will be falling too slow after a jump. The **`z`** axis has `0` because we don't even use that one.

It's important to know that every physics logic has to be made in the **_`FixedUpdate`_** method.

#


