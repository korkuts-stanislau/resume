using BinaryTrees;
using EasyConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Pages
{
    class MainPage
    {
        private BinaryTree<IntWrapper> _tree;
        private Menu _menu;
        private bool _isInterupted = false;
        public MainPage()
        {
            _menu = new Menu()
            .Add("Создать новое дерево", () => Create())
            .Add("Добавить элемент в дерево", () => Add())
            .Add("Удалить элемент из дерева", () => Delete())
            .Add("Вывести дерево в другой форме", () => Show())
            .Add("Вычислить количество листьев на определенном слое", () => GetLayerLeaves())
            .Add("Повернуть дерево относительно узла", () => Rotate())
            .Add("Вывести узлы с одним потомком", () => GetAllNodesWithOneChild())
            .Add("Загрузить тестовое дерево", () => LoadTestTree())
            .Add("Выход", () => _isInterupted = true);

            MainLoop();
        }

        private void MainLoop()
        {
            while(!_isInterupted)
            {
                if(_tree != null)
                {
                    Output.WriteLine(_tree.GetTextForPrinting(PrintFormat.AsTree));
                }
                _menu.Display();
                Console.Clear();
            }
        }

        private enum TreeType
        {
            Узловое,
            Массив,
            Отмена
        }
        private enum PrintType
        {
            Таблица,
            Список
        }
        void Create()
        {
            TreeType type = Input.ReadEnum<TreeType>("Выберите дерево какого типа создать");
            switch(type)
            {
                case TreeType.Узловое:
                    _tree = new NodeTree<IntWrapper>();
                    break;
                case TreeType.Массив:
                    _tree = new ArrayTree<IntWrapper>();
                    break;
                case TreeType.Отмена:
                    return;
                default:
                    Output.WriteLine("Что-то пошло не так");
                    return;
            }
            Input.ReadString("Дерево успешно создано. Нажмите Enter для продолжения");
        }
        void Add()
        {
            if(_tree == null)
            {
                Input.ReadString("Сначала создайте дерево. Нажмите Enter для продолжения");
                return;
            }
            int number = Input.ReadInt("Введите число, которое хотите добавить", int.MinValue, int.MaxValue);
            _tree.Add(new IntWrapper(number));
            Input.ReadString("Нажмите Enter для продолжения");
        }
        void Delete()
        {
            if (_tree == null)
            {
                Input.ReadString("Сначала создайте дерево и добавьте туда элементы. Нажмите Enter для продолжения");
                return;
            }
            int number = Input.ReadInt("Введите число, которое хотите удалить", int.MinValue, int.MaxValue);
            try
            {
                _tree.Remove(new IntWrapper(number));
            }
            catch(Exception exception)
            {
                Input.ReadString($"Ошибка! {exception.Message}\nНажмите Enter для продолжения");
                return;
            }
            Input.ReadString("Нажмите Enter для продолжения");
        }
        void Show()
        {
            if (_tree == null)
            {
                Input.ReadString("Сначала создайте дерево. Нажмите Enter для продолжения");
                return;
            }
            PrintType type = Input.ReadEnum<PrintType>("Выберите в какой форме выводить дерево");
            switch (type)
            {
                case PrintType.Таблица:
                    Output.WriteLine(_tree.GetTextForPrinting(PrintFormat.AsTable));
                    break;
                case PrintType.Список:
                    Output.WriteLine(_tree.GetTextForPrinting(PrintFormat.AsList));
                    break;
                default:
                    Output.WriteLine("Что-то пошло не так");
                    return;
            }
            Input.ReadString("Нажмите Enter для продолжения");
        }
        void GetLayerLeaves()
        {
            if (_tree == null)
            {
                Input.ReadString("Сначала создайте дерево. Нажмите Enter для продолжения");
                return;
            }
            int layer = Input.ReadInt("Введите номер уровня, на котором хотите вычислить число листьев", 0, int.MaxValue);
            Input.ReadString($"Количество листьев на уровне {layer} равно {_tree.GetLayerLeaves(layer)}\nНажмите Enter для продолжения");
        }
        private enum Rotation 
        { 
            Направо,
            Налево,
            Отмена
        }
        void Rotate()
        {
            if (_tree == null)
            {
                Input.ReadString("Сначала создайте дерево. Нажмите Enter для продолжения");
                return;
            }
            int number = Input.ReadInt("Введите значения узла, относительно которого хотите повернуть", int.MinValue, int.MaxValue);
            Rotation rot = Input.ReadEnum<Rotation>("Введите в какую сторону относительно узла повернуть");
            try
            {
                switch (rot)
                {
                    case Rotation.Направо:
                        _tree.RotateByNodeWithValue(new IntWrapper(number), BinaryTrees.Rotation.Right);
                        break;
                    case Rotation.Налево:
                        _tree.RotateByNodeWithValue(new IntWrapper(number), BinaryTrees.Rotation.Left);
                        break;
                    case Rotation.Отмена:
                        return;
                    default:
                        Output.WriteLine("Что-то пошло не так");
                        return;
                }
            }
            catch(Exception exception)
            {
                Input.ReadString($"Ошибка! {exception.Message}\nНажмите Enter для продолжения");
                return;
            }
            Input.ReadString($"Дерево успешно повёрнуто. Нажмите Enter для продолжения");
        }
        void GetAllNodesWithOneChild()
        {
            if (_tree == null)
            {
                Input.ReadString("Сначала создайте дерево. Нажмите Enter для продолжения");
                return;
            }
            List<IntWrapper> nums = _tree.GetAllOneChildNodes();
            StringBuilder builder = new StringBuilder();
            foreach(var num in nums)
            {
                builder.Append(num.ToString() + "; ");
            }
            builder.Remove(builder.Length - 2, 2);
            Input.ReadString($"Количество узлов с одним потомком равно {nums.Count}.\nУзлы {builder}.\nНажмите Enter для продолжения");
        }
        void LoadTestTree()
        {
            _tree = new NodeTree<IntWrapper>();
            IntWrapper[] values = new IntWrapper[]
            {
                new IntWrapper(40), new IntWrapper(60), new IntWrapper(70), new IntWrapper(55), new IntWrapper(52),
                new IntWrapper(45), new IntWrapper(20), new IntWrapper(65), new IntWrapper(80), new IntWrapper(20),
                new IntWrapper(35), new IntWrapper(25), new IntWrapper(13)
            };
            _tree.Add(values);
        }
    }
}
