# Ce dossier contient des fichiers word utilisés pour les tests

la regex utilisé pour ce test est "\btest\b"

|fichier|nombre d'occurence correct| nombre de d'occurence (avec piège)|constaté|commentaire|
|:-:|:-:|:-:|:-:|:-:|
|evenement.docx|1|1|1|☑|
|proteine.docx|0|1|0|☑|
|random info.docx|4|4|4|☑|
|rocade de bordeaux|0|1|0|☑|
|test wikipédia.docx|22|29|24|☒ le mot test est présent dans des liens non affichés sur word|
|test.docx|1|1|1|☑|
|histoire de france.docx|0|0|0|☑|
|histoire de l'angleterre.docx|0|0|0|☑|
|dossier/test_dossier.docx| 1 | 1 |1|☑|