--create table [#tempFAAT_APP_Comments] (
--[id] [int] identity,
--[[User_ID]] [int] NULL,
--[[Application_ID]] [int] NULL,
--[Comments] [varchar] (max) NULL,
--[Amount] [int] NULL,
--[Class_ID] [int] NULL);



set identity_insert [#tempFAAT_APP_Comments] on;


insert [#tempFAAT_APP_Comments] ([id],[[User_ID]],[[Application_ID]],[Comments],[Amount],[Class_ID])
select 1242,5033,7087,'Yes i think we can give them scholarship',30000,5619 UNION ALL
select 1243,5033,7032,'Yes i think we can give them scholarship',30000,5636 UNION ALL
select 1244,5033,7033,'Yes i think we can give them scholarship',30000,5636 UNION ALL
select 1245,5033,7045,'Yes i think we can give them scholarship',30000,5645 UNION ALL
select 1246,5033,7089,'Yes i think we can give them scholarship',30000,5665 UNION ALL
select 1247,5033,7034,'Yes i think we can give them scholarship',30000,5665 UNION ALL
select 1248,5033,7043,'Yes i think we can give them scholarship',30000,5669 UNION ALL
select 1249,5033,7058,'Yes i think we can give them scholarship',30000,5681 UNION ALL
select 1250,5033,7059,'Yes i think we can give them scholarship',30000,5686 UNION ALL
select 1251,5033,7060,'Yes i think we can give them scholarship',30000,5679 UNION ALL
select 1252,5033,7089,'Yes i think we can give them scholarship',30000,5693 UNION ALL
select 1253,5033,7087,'Yes i think we can give them scholarship',30000,5679 UNION ALL
select 1254,5033,7070,'Yes i think we can give them scholarship',30000,5679 UNION ALL
select 1255,5033,7071,'Yes i think we can give them scholarship',30000,5679 UNION ALL
select 1256,5033,7072,'Yes i think we can give them scholarship',30000,5686 UNION ALL
select 1257,4027,7087,'Yes i think we can give them scholarship',30000,5619 UNION ALL
select 1258,4027,7032,'Yes i think we can give them scholarship',30000,5636 UNION ALL
select 1259,4027,7033,'Yes i think we can give them scholarship',30000,5636 UNION ALL
select 1260,4027,7045,'Yes i think we can give them scholarship',30000,5645 UNION ALL
select 1261,4027,7089,'Yes i think we can give them scholarship',30000,5665 UNION ALL
select 1262,4027,7034,'Yes i think we can give them scholarship',30000,5665 UNION ALL
select 1263,4027,7043,'Yes i think we can give them scholarship',30000,5669 UNION ALL
select 1264,4027,7058,'Yes i think we can give them scholarship',30000,5681 UNION ALL
select 1265,4027,7059,'Yes i think we can give them scholarship',30000,5686 UNION ALL
select 1266,4027,7060,'Yes i think we can give them scholarship',30000,5679 UNION ALL
select 1267,4027,7089,'Yes i think we can give them scholarship',30000,5693 UNION ALL
select 1268,4027,7087,'Yes i think we can give them scholarship',30000,5679 UNION ALL
select 1269,4027,7070,'Yes i think we can give them scholarship',30000,5679 UNION ALL
select 1270,4027,7071,'Yes i think we can give them scholarship',30000,5679 UNION ALL
select 1271,4027,7072,'Yes i think we can give them scholarship',30000,5686 UNION ALL
select 1272,5027,7087,'Yes its fine',30000,5619 UNION ALL
select 1273,5027,7032,'Yes its fine',30000,5636 UNION ALL
select 1274,5027,7033,'Yes its fine',30000,5636 UNION ALL
select 1275,5027,7045,'Yes its fine',30000,5645 UNION ALL
select 1276,5027,7089,'Yes its fine',30000,5665 UNION ALL
select 1277,5027,7034,'Yes its fine',30000,5665 UNION ALL
select 1278,5027,7043,'Yes its fine',30000,5669 UNION ALL
select 1279,5027,7058,'Yes its fine',30000,5681 UNION ALL
select 1280,5027,7059,'Yes its fine',30000,5686 UNION ALL
select 1281,5027,7060,'Yes its fine',30000,5679 UNION ALL
select 1282,5027,7089,'Yes its fine',30000,5693 UNION ALL
select 1283,5027,7087,'Yes its fine',30000,5679 UNION ALL
select 1284,5027,7070,'Yes its fine',30000,5679 UNION ALL
select 1285,5027,7071,'Yes its fine',30000,5679 UNION ALL
select 1286,5027,7072,'Yes its fine',30000,5686 UNION ALL
select 1287,4027,7044,'GPA is Low',0,5617 UNION ALL
select 1288,4027,7050,'GPA is Low',0,5641 UNION ALL
select 1289,4027,7062,'GPA is Low',0,5642 UNION ALL
select 1290,4027,7088,'GPA is Low',0,5642 UNION ALL
select 1291,4027,7039,'GPA is Low',0,5654 UNION ALL
select 1292,4027,7051,'GPA is Low',0,5657 UNION ALL
select 1293,4027,7040,'GPA is Low',0,5658 UNION ALL
select 1294,4027,7041,'GPA is Low',0,5659 UNION ALL
select 1295,4027,7036,'GPA is Low',0,5665 UNION ALL
select 1296,4027,7090,'GPA is Low',0,5665 UNION ALL
select 1297,4027,7085,'Yes he is a good Boy',40000,5665 UNION ALL
select 1298,4027,7066,'GPA is Low',0,5669 UNION ALL
select 1299,4027,7052,'GPA is Low',0,5701 UNION ALL
select 1300,4027,7053,'GPA is Low',0,5705 UNION ALL
select 1301,4027,7054,'GPA is Low',0,5682 UNION ALL
select 1302,4027,7055,'GPA is Low',0,5675 UNION ALL
select 1303,4027,7056,'GPA is Low',0,5684 UNION ALL
select 1304,4027,7057,'GPA is Low',0,5711 UNION ALL
select 1305,4027,7061,'GPA is Low',0,5679 UNION ALL
select 1306,4027,7062,'GPA is Low',0,5676 UNION ALL
select 1307,4027,7090,'GPA is Low',0,5687 UNION ALL
select 1308,4027,7085,'GPA is Low',0,5693 UNION ALL
select 1309,4027,7066,'GPA is Low',0,5707 UNION ALL
select 1310,4027,7088,'GPA is Low',0,5677 UNION ALL
select 1311,4027,7082,'GPA is Low',0,5684 UNION ALL
select 1312,4027,7073,'GPA is Low',0,5705 UNION ALL
select 1313,4027,7086,'GPA is Low',0,5676 UNION ALL
select 1314,4027,7084,'GPA is Low',0,5676 UNION ALL
select 1315,4027,7079,'GPA is Low',0,5684 UNION ALL
select 1316,4027,7078,'GPA is Low',0,5671 UNION ALL
select 1317,5027,7044,'Grades are not appropiate',0,5617 UNION ALL
select 1318,5027,7050,'Grades are not appropiate',0,5641 UNION ALL
select 1319,5027,7062,'Grades are not appropiate',0,5642 UNION ALL
select 1320,5027,7088,'Grades are not appropiate',0,5642 UNION ALL
select 1321,5027,7039,'Grades are not appropiate',0,5654 UNION ALL
select 1322,5027,7051,'Grades are not appropiate',0,5657 UNION ALL
select 1323,5027,7040,'Grades are not appropiate',0,5658 UNION ALL
select 1324,5027,7041,'Grades are not appropiate',0,5659 UNION ALL
select 1325,5027,7036,'Grades are not appropiate',0,5665 UNION ALL
select 1326,5027,7090,'Grades are not appropiate',0,5665 UNION ALL
select 1327,5027,7085,'Grades are not appropiate',0,5665 UNION ALL
select 1328,5027,7066,'Grades are not appropiate',0,5669 UNION ALL
select 1329,5027,7052,'Grades are not appropiate',0,5701 UNION ALL
select 1330,5027,7053,'Grades are not appropiate',0,5705 UNION ALL
select 1331,5027,7054,'Grades are not appropiate',0,5682 UNION ALL
select 1332,5027,7055,'Grades are not appropiate',0,5675 UNION ALL
select 1333,5027,7056,'Grades are not appropiate',0,5684 UNION ALL
select 1334,5027,7057,'Grades are not appropiate',0,5711 UNION ALL
select 1335,5027,7061,'Grades are not appropiate',0,5679 UNION ALL
select 1336,5027,7062,'Grades are not appropiate',0,5676 UNION ALL
select 1337,5027,7090,'Grades are not appropiate',0,5687 UNION ALL
select 1338,5027,7085,'Grades are not appropiate',0,5693 UNION ALL
select 1339,5027,7066,'Grades are not appropiate',0,5707 UNION ALL
select 1340,5027,7088,'Grades are not appropiate',0,5677 UNION ALL
select 1341,5027,7082,'Grades are not appropiate',0,5684 UNION ALL
select 1342,5027,7073,'Grades are not appropiate',0,5705 UNION ALL
select 1343,5027,7086,'Grades are not appropiate',0,5676 UNION ALL
select 1344,5027,7084,'Grades are not appropiate',0,5676 UNION ALL
select 1345,5027,7079,'Grades are not appropiate',0,5684 UNION ALL
select 1346,5027,7078,'Grades are not appropiate',0,5671 UNION ALL
select 1347,5033,7044,'He is not needy',0,5617 UNION ALL
select 1348,5033,7050,'He is not needy',0,5641 UNION ALL
select 1349,5033,7062,'He is not needy',0,5642 UNION ALL
select 1350,5033,7088,'He is not needy',0,5642 UNION ALL
select 1351,5033,7039,'He is not needy',0,5654 UNION ALL
select 1352,5033,7051,'He is not needy',0,5657 UNION ALL
select 1353,5033,7040,'He is not needy',0,5658 UNION ALL
select 1354,5033,7041,'He is not needy',0,5659 UNION ALL
select 1355,5033,7036,'He is not needy',0,5665 UNION ALL
select 1356,5033,7090,'He is not needy',0,5665 UNION ALL
select 1357,5033,7085,'He is not needy',0,5665 UNION ALL
select 1358,5033,7066,'He is not needy',0,5669 UNION ALL
select 1359,5033,7052,'He is not needy',0,5701 UNION ALL
select 1360,5033,7053,'He is not needy',0,5705 UNION ALL
select 1361,5033,7054,'He is not needy',0,5682 UNION ALL
select 1362,5033,7055,'He is not needy',0,5675 UNION ALL
select 1363,5033,7056,'He is not needy',0,5684 UNION ALL
select 1364,5033,7057,'He is not needy',0,5711 UNION ALL
select 1365,5033,7061,'He is not needy',0,5679 UNION ALL
select 1366,5033,7062,'He is not needy',0,5676 UNION ALL
select 1367,5033,7090,'He is not needy',0,5687 UNION ALL
select 1368,5033,7085,'He is not needy',0,5693 UNION ALL
select 1369,5033,7066,'He is not needy',0,5707 UNION ALL
select 1370,5033,7088,'He is not needy',0,5677 UNION ALL
select 1371,5033,7082,'He is not needy',0,5684 UNION ALL
select 1372,5033,7073,'He is not needy',0,5705 UNION ALL
select 1373,5033,7086,'He is not needy',0,5676 UNION ALL
select 1374,5033,7084,'He is not needy',0,5676 UNION ALL
select 1375,5033,7079,'He is not needy',0,5684 UNION ALL
select 1376,5033,7078,'He is not needy',0,5671 UNION ALL
select 1377,4027,7085,'Yes He deserves',40000,NULL UNION ALL
select 1378,4027,7090,'Yes fine',40000,NULL UNION ALL
select 1379,4027,7078,'Yes',50000,5715 UNION ALL
select 1380,5027,7078,'Yes ok ',40000,5715 UNION ALL
select 1381,4027,7089,'Yes she is Eligible',20000,5754 UNION ALL
select 1382,5027,7089,'Yes She is Eligible',40000,5754 UNION ALL
select 1383,4027,7100,'I think he deserves the Scholarship',40000,5734 UNION ALL
select 1384,4027,7034,'She is deserving',40000,5754 UNION ALL
select 1385,4027,8101,'Yes he is Eligible',20000,5735;

set identity_insert [#tempFAAT_APP_Comments] off;