Select * from Personals;

Select * from Personals
where Befattning = 'Lärare';

Select * from Betygs
where Datum >= '2023-01-19'
order by Datum desc;

Select kurses.Namn, AVG(Betygbetyg) AS avg_betyg, MAX(Betygbetyg) AS max_betyg, MIN(Betygbetyg) AS min_betyg
from Betygs
JOIN Kurses ON Betygs.KursID = Kurses.ID
Group By kurses.Namn;

Insert into Students(Namn,Efternamn,Personnummer,KlassID)
Values ('Peter','Pan','19530205-1902',2);