# OffcieFinder
Permet de rechercher des informations au sein d'un ou de plusieurs fichiers word.

## Méthode de recherche (REGEX)
Les recherches fonctionnent avec le principe de regex (éxpression régulière) qui permettent de définir précisement une recherche.

Les regex peuvent devenir complexe en fonction de l'information recherchée. SI ovus rechercher un caractère, un mot ou une phrase, vous pouvez les saisir de façon classique. Les regex permettent d'affiner la recherche en rajoutant des informations complémentaires.

Le site https://regex101.com/  permet de créer et de tester les regex.

recherche de bonjour : `bonjour`

recherche de la phrase "Je souhaite rechercher un mot" : `Je souhaite rechercher un mot`

L'interêt d'une regex est de pouvoir définir plus finement la recherche que simplement mettre une suite de caractère :

 recherche du mot ce : `\bce\b`

 Ici le caractère \b signifie bordure d'un mot. La regex ne renverra donc que les mots ce et non pas les mots comprenant ce.

 |mot recherché| ce | " ce "  | `\bce\b`|résultat attendu|
 |:------:|:------:|:---------:|:----:|:---:|
 | se | <span style="color:green">☒</span> | <span style="color:green">☒</span> |<span style="color:green">☒</span> | <span style="color:RGB(0,150,255)">☒</span>|
 | ce matin | <span style="color:green">☑</span> | <span style="color:green">☑</span> |<span style="color:green">☑</span> |<span style="color:RGB(0,150,255)">☑</span>|
 |ceci | <span style="color:red">☑</span> | <span style="color:green">☒</span>|<span style="color:green">☒</span> |<span style="color:RGB(0,150,255)">☒</span>|
 |.Ce| <span style="color:green">☑</span> | <span style="color:red">☒</span> | <span style="color:green">☑</span> |<span style="color:RGB(0,150,255)">☑</span>| 



 La regex est ainsi plus flexible

La séléction des options (multiligne, case, ...) n'est pas encore configurable dans les options du programme. Ils sont pour le moment saisie directement dans le code.

## Utilisation de l'outil
### Rechercher dans un fichier unique :
- forme longue : `OfficeFinder.exe --regex <regex> --path <filePath>`
- forme abbrégée : `OfficeFinder.exe -r <regex> -p <filePath>`
### Rechercher dans l'ensemble des fichiers d'un répertoire
- forme longue : `OfficeFinder.exe --regex <regex> --path <DirectoryPath>`
- forme abbrégée : `OfficeFinder.exe -r <regex> -p <DirectoryPath>`

ou : OfficeFinder.exe -r <regex> -p <path>

`<regex>` : Regex définissant l'élément recherché dans le fichier

`<path>` : Chemin vers le fichier cible

Pour créer des regex vous pouvez utiliser le site https://regex101.com/ 


## example

- recherche de mot :
    - `OfficeFinder.exe --regex "bonjour" --path ./documentWord.docx`
    - `OfficeFinder.exe --regex "bonjour" --path ./directory`
    - `OfficeFinder.exe --regex "bonjour"`

- recherche de phrase :
    - `OfficeFinder.exe --regex "Ceci est ma recherche" --path ./documentWord.docx`
    - `OfficeFinder.exe --regex "Ceci est ma recherche" --path ./directory`
    - `OfficeFinder.exe --regex "Ceci est ma recherche"`

- recherche avec une regex plus compliqué 
    - `OfficeFinder.exe --regex "Ceci est ma recherche" --path ./documentWord.docx`
    - `OfficeFinder.exe --regex "Ceci est ma recherche" --path ./directory`
    - `OfficeFinder.exe --regex "Ceci est ma recherche"`
