# Singleton design pattern
[Singleton](https://en.wikipedia.org/wiki/Singleton_pattern) is one of the [creational patterns](https://en.wikipedia.org/wiki/Creational_pattern). This repository contains only one example for it.
Sometimes we need a class which has only one instance. It is helpful to handle files or database.

## When should you use it
As I mentioned you can use it when you want to avoid more instance handle the same resource.
But you have to be careful because everybody can use this instance and can perform some changes on it. It can cause some surprises.
To write unit test for this class is not easy or I would say it is impossible. I created a unit test but just for to check is it singleton or not.

## How to create a singleton class?
1. Create a private(!) consturctor to ensure anybody else instantiate it.
2. Create a private variable which contains the only instance
3. Create a public static field which can give back the only instance

### Like this
    public class MySingletonClass
    {
      public static MySingletonClass GetInstance
      {
        get
        {
          return instance;
        }
      }

      private static readonly MySingletonClass instance = new MySingletonClass();

      private MySingletonClass()
      {
      }
    }
