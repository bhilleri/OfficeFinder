# OffcieFinder
Permet de rechercher des informations dans plusieurs fichiers office

## Utilisation de l'outil
taper la commande OfficeFinder.exe --regex <regex> --path <path>
ou : OfficeFinder.exe -r <regex> -p <path>

<regex> : Regex définissant l'élément recherché dans le fichier
<path> : Chemin vers le fichier cible

Pour créer des regex vous pouvez utiliser le site https://regex101.com/ 
Pour le moment la recherche est uniquement multi-ligne. Il n'est pas encore possible de venir définir des options supplémentaires dans la regex.
Si vous recherez un mot vous pouvez directement le tapez.
Si vous tapez une phrase ou que votre recherche comprend des espaces, il faut rajouter des guillemets autours autours de celle-ci

## example

- recherche de mot :
OfficeFinder.exe bonjour ./documentWord.docx

- recherche de phrase :
OfficeFinder.exe "Ceci est ma recherche" ./documentWord.docx

- recherche avec une regex plus compliqué 
OfficeFinder.exe "Ceci est ma recherche" ./documentWord.docx
