using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarAnimals.Classes
{
    class Animal
    {
        int _id;
        int _hpMax;
        int _hp;
        int _dm;
        int _exp;
        int _lvl;
        int _expApp;
        int _x;
        int _y;
        int _maxX;
        int _maxY;
        int _takt;
        bool _att;
        int _standingStill;
        public int X=>_x;
        public int Y => _y;
        public int Lvl => _lvl;
        public Animal(int id,int width,int height)
        {
            _maxX = width-1;
            _maxY = height-1;
            _id = id;
            _lvl = 1;
            _hp = 3;
            _hpMax = 3;
            _dm = 1;
            _att = false;
            _exp = 0;
            _expApp = 2 * _lvl;
            Random r = new Random(id);
            _x = r.Next(1, _maxX-1);
            _y = r.Next(1, _maxY-1);
            _takt = 0;
            _standingStill = 0;
        }
        public void Go()
        {
            Random r0 = new Random();
            Random r =new Random(_id+_x+_y+r0.Next(10000));
            int dx = r.Next(-1, 2);
            int dy = r.Next(-1, 2);
            if(dx==0 && dy == 0) _standingStill++; else _standingStill = 0;
            if (_standingStill > 3)
            {    
                    dx = r0.Next(-1, 2); 
                    dy = r0.Next(-1, 2);
                _standingStill = 0;
            }
            _takt++;
            if (_x + dx <= _maxX && _x + dx>0) _x += dx; else _x -= dx;
            if (_y + dy <= _maxY && _y + dy>0) _y += dy; else _y -= dy;
            _att = true;
           
        }// сделали шаг и отсчитали такт 
        public int GetAtt()
        {
            if (_att&& _hp>0)
            {
                _att = false;
                return _dm;
            }
            else
                return 0;
        }// возвращаем значение атаки
        public int GetExpSetDm(int d)
        {
            _hp -= d;
            if (_hp>0)
            {
                return 0;
            }
            else
            {
                return _lvl;
            }
        }// принимаем на себя дамаг и возврвшвем опыт
        public void SetExp(int exp)
        {
            _exp += exp;
            while (_exp > _expApp && CheckDeath())
            {
                _exp -= _expApp;
                _lvl++;
                _expApp = _lvl * 2;
                _hpMax++;
                _hp = _hpMax;
                if (_lvl % 3 == 0) _dm++;
            }
        }// принимаем опыт 
        public bool CheckDeath()
        {
            if (_hp > 0) return true; else return false;
        }// проверяем жазни
        public char Sprite()
        {
            if (Console.CapsLock)
            {
                return _lvl.ToString()[0];
            }
            else
            {
                return '@';
            }
        }//возвращаем картинку 
        public bool CheckNewLife()
        {
            if (_takt % 50 == 0)
            {
                _hp--;
                return true;
            }
            else
                return false;
        }//проверяем рождение потомка 

        


    }
}
