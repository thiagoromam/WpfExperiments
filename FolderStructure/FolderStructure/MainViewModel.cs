using System.Windows.Input;
using FolderStructure.Components;
using FolderStructure.Models;

namespace FolderStructure
{
    public class MainViewModel : NotificationObject
    {
        private readonly ExtendedObservableCollection<ISpriteSheetNode> _spriteSheets;
        private bool _charactersAdded;
        private bool _scenariosAdded;
        private bool _unusedTestAdded;
        private bool _knucklesAddedToHeroes;
        private bool _villainsAddedToCharacters;
        private bool _metalSonicAddedToVillains;
        private SpriteSheetFolder _characters;
        private SpriteSheetFolder _heroes;
        private SpriteSheetFolder _scenarios;
        private SpriteSheet _unusedTest;
        private SpriteSheet _knuckles;
        private SpriteSheetFolder _villains;
        private SpriteSheet _metalSonic;

        public MainViewModel()
        {
            _spriteSheets = new ExtendedObservableCollection<ISpriteSheetNode>();

            Nodes = new SpriteSheetsCollection(_spriteSheets);

            AddCharactersCommand = new DelegateCommand(o => AddCharacters(), o => !CharactersAdded);
            AddScenariosCommand = new DelegateCommand(o => AddScenarios(), o => !ScenariosAdded);
            AddUnusedTestCommand = new DelegateCommand(o => AddUnusedTest(), o => !UnusedTestAdded);
            AddKnucklesToHeroesCommand = new DelegateCommand(o => AddKnucklesToHeroes(), o => CharactersAdded && !KnucklesAddedToHeroes);
            AddVillainsToCharactersCommand = new DelegateCommand(o => AddVillainsToCharacters(), o => CharactersAdded && !VillainsAddedToCharacters);
            AddMetalSonicToVillainsCommand = new DelegateCommand(o => AddMetalSonicToVillains(), o => VillainsAddedToCharacters && !MetalSonicAddedToVillains);
            RemoveCharactersCommand = new DelegateCommand(o => RemoveCharacters(), o => CharactersAdded);
            RemoveScenariosCommand = new DelegateCommand(o => RemoveScenarios(), o => ScenariosAdded);
            RemoveUnusedTestCommand = new DelegateCommand(o => RemoveUnusedTest(), o => UnusedTestAdded);
            RemoveKnucklesFromHeroesCommand = new DelegateCommand(o => RemoveKnucklesFromHeroes(), o => KnucklesAddedToHeroes);
            RemoveVillainsFromCharactersCommand = new DelegateCommand(o => RemoveVillainsFromCharacters(), o => VillainsAddedToCharacters);
            RemoveMetalSonicFromVillainsCommand = new DelegateCommand(o => RemoveMetalSonicFromVillains(), o => MetalSonicAddedToVillains);
        }

        public SpriteSheetsCollection Nodes { get; }

        // Commands
        public ICommand AddCharactersCommand { get; }
        public ICommand AddScenariosCommand { get; }
        public ICommand AddUnusedTestCommand { get; }
        public ICommand AddKnucklesToHeroesCommand { get; }
        public ICommand AddVillainsToCharactersCommand { get; }
        public ICommand AddMetalSonicToVillainsCommand { get; }
        public ICommand RemoveCharactersCommand { get; }
        public ICommand RemoveScenariosCommand { get; }
        public ICommand RemoveUnusedTestCommand { get; }
        public ICommand RemoveKnucklesFromHeroesCommand { get; }
        public ICommand RemoveVillainsFromCharactersCommand { get; }
        public ICommand RemoveMetalSonicFromVillainsCommand { get; }

        // Verifications
        public bool CharactersAdded
        {
            get { return _charactersAdded; }
            private set
            {
                if (value == _charactersAdded) return;
                _charactersAdded = value;
                OnPropertyChanged();
            }
        }
        public bool ScenariosAdded
        {
            get { return _scenariosAdded; }
            private set
            {
                if (value == _scenariosAdded) return;
                _scenariosAdded = value;
                OnPropertyChanged();
            }
        }
        public bool UnusedTestAdded
        {
            get { return _unusedTestAdded; }
            private set
            {
                if (value == _unusedTestAdded) return;
                _unusedTestAdded = value;
                OnPropertyChanged();
            }
        }
        public bool KnucklesAddedToHeroes
        {
            get { return _knucklesAddedToHeroes; }
            private set
            {
                if (value == _knucklesAddedToHeroes) return;
                _knucklesAddedToHeroes = value;
                OnPropertyChanged();
            }
        }
        public bool VillainsAddedToCharacters
        {
            get { return _villainsAddedToCharacters; }
            private set
            {
                if (value == _villainsAddedToCharacters) return;
                _villainsAddedToCharacters = value;
                OnPropertyChanged();
            }
        }
        public bool MetalSonicAddedToVillains
        {
            get { return _metalSonicAddedToVillains; }
            private set
            {
                if (value == _metalSonicAddedToVillains) return;
                _metalSonicAddedToVillains = value;
                OnPropertyChanged();
            }
        }

        private void AddCharacters()
        {
            _characters = new SpriteSheetFolder("Characters");
            _heroes = new SpriteSheetFolder("Heroes")
            {
                new SpriteSheet("Sonic"),
                new SpriteSheet("Tails")
            };

            _characters.Add(_heroes);
            _spriteSheets.Add(_characters);

            CharactersAdded = true;
        }
        private void AddScenarios()
        {
            _scenarios = new SpriteSheetFolder("Scenarios")
            {
                new SpriteSheet("Emerald Hill Zone"),
                new SpriteSheet("Chemical Plant Zone"),
                new SpriteSheet("Aquatic Ruin Zone")
            };
            _spriteSheets.Add(_scenarios);

            ScenariosAdded = true;
        }
        private void AddUnusedTest()
        {
            _unusedTest = new SpriteSheet("Unused (Test)");
            _spriteSheets.Add(_unusedTest);

            UnusedTestAdded = true;
        }
        private void AddKnucklesToHeroes()
        {
            _knuckles = new SpriteSheet("Knuckles");
            _heroes.Add(_knuckles);

            KnucklesAddedToHeroes = true;
        }
        private void AddVillainsToCharacters()
        {
            _villains = new SpriteSheetFolder("Villains")
            {
                new SpriteSheet("Robotnik")
            };
            _characters.Add(_villains);

            VillainsAddedToCharacters = true;
        }
        private void AddMetalSonicToVillains()
        {
            _metalSonic = new SpriteSheet("Metal Sonic");
            _villains.Add(_metalSonic);

            MetalSonicAddedToVillains = true;
        }
        private void RemoveCharacters()
        {
            RemoveKnucklesFromHeroes();
            RemoveVillainsFromCharacters();

            _spriteSheets.Remove(_characters);
            _characters = null;
            _heroes = null;

            CharactersAdded = false;
        }
        private void RemoveScenarios()
        {
            _spriteSheets.Remove(_scenarios);
            _scenarios = null;

            ScenariosAdded = false;
        }
        private void RemoveUnusedTest()
        {
            _spriteSheets.Remove(_unusedTest);
            _unusedTest = null;

            UnusedTestAdded = false;
        }
        private void RemoveKnucklesFromHeroes()
        {
            _heroes.Remove(_knuckles);
            _knuckles = null;

            KnucklesAddedToHeroes = false;
        }
        private void RemoveVillainsFromCharacters()
        {
            RemoveMetalSonicFromVillains();

            _characters.Remove(_villains);
            _villains = null;

            VillainsAddedToCharacters = false;
        }
        private void RemoveMetalSonicFromVillains()
        {
            _villains.Remove(_metalSonic);
            _metalSonic = null;

            MetalSonicAddedToVillains = false;
        }
    }
}