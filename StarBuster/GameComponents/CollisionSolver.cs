using StarBuster.GameComponents;
using StarBuster.Objects2D;

public class CollisionSolver
{
    public void ResolveCollisions(List<(Object2D, Object2D)> collisions)
    {
        foreach (var (obj1, obj2) in collisions)
        {
            if ((obj1 is Hero && obj2 is Enemy) || (obj1 is Enemy && obj2 is Hero))
            {
                // Jeśli Enemy uderzył w Hero, odejmij 20 HP od Hero
                if (obj1 is Hero hero1 && obj2 is Enemy)
                {
                    hero1.Hitted(20);
                }
                else if (obj1 is Enemy && obj2 is Hero hero2)
                {
                    hero2.Hitted(20);
                }

                // Wywołanie eksplozji, jeśli Enemy trafił w Hero
                TriggerExplosion(obj2);

                // Usuwamy Enemy po uderzeniu
                GameManager.Instance.Remove(obj2);
            }
            else if ((obj1 is Bullet  && obj2 is Enemy ) || (obj1 is Enemy  && obj2 is Bullet ))
            {
                if (obj1 is Bullet bullet1 && obj2 is Enemy )
                {
                    TriggerExplosion(obj2);  
                    GameManager.Instance.Remove(obj2);
                    GameManager.Instance.Remove(bullet1);
                }
                else if (obj1 is Enemy && obj2 is Bullet bullet3)
                {
                    TriggerExplosion(obj1);  
                    GameManager.Instance.Remove(obj1);
                    GameManager.Instance.Remove(bullet3);  
                }
            }
            else if ((obj1 is Hero && obj2 is Boss ) || (obj1 is Boss && obj2 is Hero))
            {
                if (obj1 is Hero hero1 && obj2 is Boss boss1)
                {
                    hero1.Hitted(20);
                    boss1.Hitted(10);
                }
                else if (obj1 is Boss boss2 && obj2 is Hero hero2)
                {
                    hero2.Hitted(20);
                    boss2.Hitted(10);
                }

                TriggerExplosion(obj2);
                //hp -10
            }
            else if ((obj1 is Bullet && obj2 is Boss) || (obj1 is Boss && obj2 is Bullet))
            {
                if (obj1 is Bullet bullet2 && obj2 is Boss boss3)
                {
                    TriggerExplosion(obj2);
                    //hp-5
                    boss3.Hitted(5);
                    GameManager.Instance.Remove(bullet2);
                }
                else if (obj1 is Boss boss4 && obj2 is Bullet bullet4)
                {
                    TriggerExplosion(obj1);
                    //hp-5
                    boss4.Hitted(5);
                    GameManager.Instance.Remove(bullet4);
                }
            }
        }
    }

    private void TriggerExplosion(Object2D obj)
    {
        // Jeśli obiekt jest typu Enemy, wywołujemy eksplozję
        if (obj is Enemy enemy)
        {
            // Stworzenie eksplozji w miejscu wroga
            Explosion explosion = new Explosion(enemy.x, enemy.y);  // Przekazujemy pozycję wroga
            GameManager.Instance.AddObject2D(explosion);  // Dodajemy eksplozję do gry, aby była renderowana
        }
        // Jeśli obiekt jest Bullet, wywołujemy eksplozję na jego pozycji
        else if (obj is Bullet bullet)
        {
            // Stworzenie eksplozji w miejscu pocisku
            Explosion explosion = new Explosion(bullet.x, bullet.y);  // Przekazujemy pozycję pocisku
            GameManager.Instance.AddObject2D(explosion);  // Dodajemy eksplozję do gry
        }
        else if (obj is Boss boss5)
        {
            // Stworzenie eksplozji w miejscu wroga
            Explosion explosion = new Explosion(boss5.x, boss5.y);  // Przekazujemy pozycję wroga
            GameManager.Instance.AddObject2D(explosion);  // Dodajemy eksplozję do gry, aby była renderowana
        }
    }
}
