delete dbo.drinkCategory where id = 4

select * from Account where username = 'jjk'

SELECT COUNT(*) FROM dbo.drink WHERE name = N'Hồng trà sữa'

select max(id) from dbo.bill;

select d.name, bi.count, d.price, d.price*bi.count as totalPrice
from dbo.bill as b, dbo.billInfor as bi, dbo.drink as d
where bi.idBill = b.id and bi.idDrink = d.id
select * from dbo.billInfor

select idDrink from billInfor where idDrink = 2

select count from billInfor where idDrink = 2 and idBill = 40
select idDrink from billInfor where idDrink = 2 and idBill = 41

delete dbo.bill

exec USP_GetListBill '20240514'

select count(*) from Account where username = 'jjk' and password = '123'

