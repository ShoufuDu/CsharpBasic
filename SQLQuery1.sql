if exists ( select * from sysdatabases where name='employees')
	print 'Yes, the DB exists'
else
	print 'No, need a new one?'

create table employees(
employee_id int not null,
first_name varchar(5) not null,
last_name varchar(6) not null,
salary int not null,
hire_date date,
email varchar(20),
phone_number varchar(13),
job_id int,
commision_pct decimal(10,2),
manager_id int,
department_id　int)

alter table employees add primary key (employee_id)

insert into employees values(1,'Tom','Du',12000,'2015-02-12','tom@sina.com','12345550',111,10.2,3,10,'')
insert into employees values(2,'Tom1','Du1',13000,'2016-02-12','tom1@sina.com','12345551',111,10.3,3,10,'')
insert into employees values(3,'Tom2','Du2',14000,'2016-01-12','tom2@sina.com','12345552',111,10.4,3,10,'')
insert into employees values(4,'Tom3','Du3',15000,'2015-03-12','tom3@sina.com','12345553',111,10.5,3,10,'')
insert into employees values(5,'Tom4','Du4',16000,'2015-05-12','tom4@sina.com','12345554',111,10.6,4,11,'')
insert into employees values(6,'Tom5','Du5',17000,'2015-06-12','tom5@sina.com','12345555',111,12.7,4,11,'')
insert into employees values(7,'Tom6','Du6',18000,'2015-07-12','tom6@sina.com','12345556',112,14.2,4,11,'')
insert into employees values(8,'Tom7','Du7',19000,'2015-08-12','tom7@sina.com','12345557',112,15.2,4,11,'')
insert into employees values(9,'Tom8','Du8',21400,'2015-09-12','tom8@sina.com','12345558',112,16.2,5,12,'')
insert into employees values(10,'Tom9','Du9',12200,'2015-10-12','tom9@sina.com','12345559',112,17.2,5,12,'')
insert into employees values(11,'Tom10','Du10',12600,'2017-02-12','tom10@sina.com','123455510',112,18.2,5,12,'')
insert into employees values(12,'Tom11','Du11',13300,'2014-02-12','tom11@sina.com','123455511',112,21.2,5,12,'')
insert into employees values(13,'Tom12','Du12',32000,'2013-02-12','tom12@sina.com','123455512',112,22.2,5,12,'')
insert into employees values(14,'Tom13','Du13',14000,'2012-02-12','tom13@sina.com','123455513',112,23.2,3,10,'')

insert into employees values(15,'Tom14','Du14',15600,'2019-02-12','tom14@sina.com','123455513',112,23.2,6,17,'')

select * from dbo.employees

alter table employees add primary key(employee_id)

delete from employees where employee_id=15

update employees set job_id=112 where employee_id>6

create table jobs(
job_id int not null,
job_title varchar(10) not null,
min_salary int,
max_salary int)

insert into jobs values(111,'tester',10000,20000)
insert into jobs values(112,'developer',10000,30000)

create table departments(
department_id int not null,
department_name varchar(10) not null,
manager_id int not null,
location_id int not null)

alter table departments add primary key(department_id)
insert into departments values(10,'developer1',3,1)
insert into departments values(11,'tester1',4,2)
insert into departments values(12,'developer2',5,3)
insert into departments values(13,'developer5',6,3)
insert into departments values(14,'developer6',7,3)

create table locations(
location_id int not null,
street varchar(12) not null,
city varchar(4) not null,
post_code varchar(5) not null,
state_province varchar(3) not null,
country_id varchar(4) not null)

alter table locations add constraint pk_id primary key(location_id)

alter table locations add primary key (location_id)

insert into locations values(1,'Big River','NJ','2011','JS','CHI')
insert into locations values(2,'High Fall','SH','2012','SH','CHI')
insert into locations values(3,'Silver RD','HK','2013','HK','CHI')

select * from locations

drop table locations

create table job_grades(
job_grade varchar(20) not null primary key,
low_salary int,
high_salary int)

-- get the department_id,department_name, count

select d.department_name, t.id,t.num
from departments d,
(select department_id as id, count(*) as num
from employees
group by department_id) t
where t.id = d.department_id


select * from employees
group by department_id

select distinct department_id from employees

-- select department_id,max salary of the department
select department_id,max(salary) as max_salary
from employees
group by department_id

--1、上月的第一天
SELECT CONVERT(CHAR(10),DATEADD(month,-1,DATEADD(dd,-DAY(GETDATE())+1,GETDATE())),111)

--2、上月的最后一天
SELECT CONVERT(CHAR(10),DATEADD(ms,-3,DATEADD(mm, DATEDIFF(mm,0,getdate()),0)),111)+' 23:59:59'

--3、本月的第一天
SELECT CONVERT(CHAR(10),DATEADD(dd,-DAY(GETDATE())+1,GETDATE()),120)

--4 本月的最后一天
SELECT CONVERT(CHAR(10),DATEADD(ms,-3,DATEADD(mm,DATEDIFF(m,0,getdate())+1,0)),120)+' 23:59:59'

--5、来月的最后第一天
SELECT CONVERT(CHAR(10),DATEADD(m,1,DATEADD(dd,-DAY(GETDATE())+1,GETDATE())),120)

--6、来月的最后一天
SELECT CONVERT(CHAR(10),DATEADD(ms,-3,DATEADD(mm,DATEDIFF(m,0,getdate())+2,0)),120)+' 23:59:59'

select cast(convert(varchar(7),dateadd(mm,1,'2012-02-04'),120) + '-01' as datetime) - 1;


select manager_id
	from employees
	where last_name='Du12'

--select the information of the person who is the manager of the person whose last_name is 'Du12'
select *
	from employees
	where employee_id=
	(select manager_id
	from employees
	where last_name='Du12'
	)

select e2.*
	from employees e1,employees e2
	where e1.manager_id=e2.employee_id and e1.last_name='Du12'

-- select the deparment_id,avg(salary) of the department whose average salary is more than 8000
select department_id,avg(salary)
	from employees
	group by department_id
	having	avg(salary)>8000

-- select the information of the department whose average salary is more than 8000
select *
from departments
where department_id in (
	select department_id
	from employees
	group by department_id
	having	avg(salary)>8000
	)

-- select the information of the employee whose salary is the lowest
select * from employees
where salary=
(select min(salary)
from employees)

-- select the information of all the departments whose average salary is more than 14000
select department_id,avg(salary)
	from employees
	group by department_id
	having avg(salary)>14000


select d.*,(select avg(salary)
					from employees
					where department_id=d.department_id) as avg_s
	from departments d
	where d.department_id = 
	(
		select department_id 
		from employees
		group by department_id
		having avg(salary)=
		(
			select min(avg_s)
			from
			(select department_id,avg(salary) as avg_s
			from employees　
			group by department_id) t1
		)
	)

	select　j.*, (select avg(salary)
					from employees
					where job_id=j.job_id) as avg_ss
	from jobs j
	where job_id=
	(
		select job_id
		from employees
		group by job_id
		having avg(salary)=
			(select max(avg_s)
			from
			(select avg(salary) as avg_s
			from employees
			group by job_id)t1
			)
	)


select department_id
		from employees
		group by department_id
		having avg(salary)>
		(select avg(salary)
			from employees)

select *
	from employees
	where employee_id in
	(select distinct manager_id
			from employees)

--各个部门中 最高工资中最低的那个部门的最低工资是多少
select department_id,min(salary)
	from employees
	group by department_id
	having max(salary)=
	(	select min(max_s)
		from 
		(select max(salary) as max_s
		from employees
		group by department_id) t1
	)

--查询平均工资最高的部门的manager 的详细信息: last_name,department_id, email, salary 
	select e.employee_id,e.first_name,e.last_name,e.email
	from employees e
	join departments d
	on e.employee_id = d.manager_id
	and d.department_id = (
	select department_id
	from employees
	group by department_id
	having avg(salary)=
		(select max(avg_s)
		from
		(select avg(salary) as avg_s
		from employees
		group by department_id) t)
)

--查询 1999 年来公司的人中所有员工的最高工资的那个员工的信息.
select *
	from employees
	where convert(varchar,datepart(year,hire_date))='2015'
	and  salary =
		(select max(salary)
		from employees
		where convert(varchar,datepart(year,hire_date))='2015')

--查询与2号或3号员工的 manager_id 和 department_id 相同的其他员工的 employee_id, manager_id, department_id
select *
from employees
where ((manager_id=(select manager_id from employees where employee_id=2) and 
	  department_id = (select department_id from employees where employee_id=2)) or
	   (manager_id=(select manager_id from employees where employee_id=3) and 
	  department_id = (select department_id from employees where employee_id=3)) ) and
	  employee_id not in (2,10)

select *
from employees
where manager_id in (select manager_id 
				from employees
				where employee_id in (2,10))
	 and department_id in (select department_id from employees where employee_id in (2,10) )
	 and employee_id not in (2,10)

SELECT * FROM sys.database_principals

select SCHEMA_NAME()

select current_user

--返回比本部门平均工资高的员工的 last_name, department_id, salary 及平均工资

select e.first_name,e.last_name,e.department_id,t.avg_s
from employees e,(select department_id,avg(salary) as avg_s
					from employees
					group by department_id) t
where e.department_id=t.department_id and e.salary>t.avg_s


select last_name,department_id,salary,
          (select avg(salary) 
           from employees e3
          where e1.department_id = e3.department_id
          ) avg_salary
from employees e1 
where salary >(select avg(salary) 
                            from employees e2 
                           where e1.department_id = e2.department_id)

select * from departments

--显示员工的employee_id,last_name 和location。其中，若员工department_id 与 location_id 为1800 的 department_id 相同，
--则location 为’Canada’,其余则为’USA’
select first_name,last_name,
	(case department_id
	 when (select department_id
	       from departments
		   where location_id=2)
	 then 'Canada'
	 else 'USA' end) location
	 from employees

 --查 询 员 工 的  employee_id,last_name, 要 求 按 照 员 工 的department_name 排序

 select e.employee_id,e.last_name,
		d.department_name
from employees e,departments d
where e.department_id=d.department_id
order by d.department_name

select e.employee_id,e.last_name
from employees e
order by (select department_name
			from departments d
			where e.department_id = d.department_id)

select e.employee_id, e.last_name
from employees e
join departments d
on e.department_id = d.department_id
order by d.department_name

--查询员工中工资大于本部门平均工资的员工的 last_name,salary 和其 department_id 
select e.last_name,e.salary,e.department_id
from employees e
where salary > (select avg(salary)
				from employees e1
				where e.department_id = e1.department_id)


--若 employees 表中employee_id 与 job_history 表中employee_id相同的数目不小于 2，
--输出这些相同 id 的员工的employee_id,last_name和其 job_id

--查询公司管理者的employee_id,last_name,job_id,department_id 信息 
select e.employee_id,last_name,e.job_id,e.department_id
from employees e
where e.employee_id in (
select distinct manager_id from employees 
)

--查询departments 表中，不存在于employees 表中的部门的department_id 和 department_name 
select department_id,department_name
from departments
where department_id not in(
select distinct department_id 
from employees)

--修改表 employees,添加department_name 列，赋予department_id相应的部门名称。
alter table employees add department_name varchar(10)

select department_name from employees

update employees
set department_name = d.department_name
from employees e, departments d
where e.department_id = d.department_id

select top(2) *
from employees
order by salary desc


--查询公司中各部门的总工资大于公司中各部门的平均总工资的部门信息
with
q_dept_total as(
select department_name,sum(salary) as dept_total
from employees
group by department_name),
q_dept_avg as(
select sum(dept_total)/count(*) as dept_avg
from q_dept_total)

select *
from q_dept_total,q_dept_avg
where dept_total>dept_avg

--选择工资大于所有  JOB_ID =  'SA_MAN' 的员工的工资的员工的last_name,job_id, salary
select * from jobs
select last_name,job_id,salary
from employees
where salary > all(
select salary
from employees
where job_id=111)

create table grades(
student_id int ,
course_id int,
course_name varchar(10),
grade int)

--用一条SQL 语句 查询出每门课都大于80 分的学生ID
insert into grades values(1,11,'Chinese',76)
insert into grades values(1,11,'Maths',87)
insert into grades values(1,11,'English',85)
insert into grades values(2,11,'Chinese',95)
insert into grades values(2,11,'Maths',87)
insert into grades values(2,11,'English',92)
insert into grades values(3,11,'Chinese',76)
insert into grades values(3,11,'Maths',79)
insert into grades values(3,11,'Englist',85)
insert into grades values(4,11,'Chinese',91)

select * from grades

select student_id
from (select student_id,min(grade) as min_grade
from grades
group by student_id) t
where min_grade>80

select student_id
from grades
group by student_id
having min(grade)>80

/*
2. 学生表 如下:
自动编号   学号   姓名 课程编号 课程名称 分数
1        2005001 张三 0001     数学    69
2        2005002 李四 0001      数学    89
3        2005001 张三 0001      数学    69
删除除了自动编号不同, 其他都相同的学生冗余信息
*/
create table grades1(
id int primary key identity,
s_id varchar(8),
name varchar(10),
c_id varchar(5),
c_name varchar(8),
grade int)

drop table grades1
select * from grades1

insert grades1 values('2005001','zhangsan','0001','Maths',69)
insert grades1 values('2005001','zhangsan','0001','Maths',69)
insert grades1 values('2005002','lise','0002','Eng',76)
insert grades1 values('2005002','lise','0002','Eng',76)
insert grades1 values('2005002','lise','0002','Eng',76)

delete from grades1
where id not in (select min(id) from grades1 group by s_id,name,c_id,c_name,grade)


--个叫 team 的表，里面只有一个字段name, 一共有4 条纪录，分别是a,b,c,d, 对应四个球对，
--现在四个球对进行比赛，用一条sql 语句显示所有可能的比赛组合
create table teams(
name varchar(10) not null primary key)

insert into teams values('red')
insert into teams values('green')
insert into teams values('yellow')
insert into teams values('black')

select t1.name,t2.name
from teams t1,teams t2
where t1.name<t2.name
		--and 
		--convert(varchar(10),t1.name)>convert(varchar(10),t2.name)


--复制表结构
select * into copy_employees from employees where 1<>1
insert into copy_employees select * from employees where 1<>1

select * from copy_employees
drop table copy_employees

--复制几列
create table copy_employees_part(
a varchar(10) primary key,
b varchar(10),
c int)

select * from copy_employees_part
drop table copy_employees_part

insert into copy_employees_part(a,b,c)
select first_name,last_name,salary
from employees

--日程安排提前五分钟提醒
create table schedules(
start_date  datetime)

insert into schedules values('2019-03-04 12:00:00')
select * from schedules

select * from schedules 
where DATEDIFF(day,start_date,getdate()) > -1

select DATEDIFF(minute,start_date,getdate())
from schedules

--注意表别名的写法
delete e from employees e
where exists(select * from departments d where e.department_id=d.department_id)

-- OK
delete from employees
where exists(select * from departments d where department_id=d.department_id)

--OK
delete from employees
where exists(select * from departments d where employees.department_id=d.department_id)

/*
原表:
courseid coursename score
-------------------------------------
1 Java 70
2 oracle 90
3 xml 40
4 jsp 30
5 servlet 80
-------------------------------------
为了便于阅读, 查询此表后的结果显式如下( 及格分数为60):
courseid coursename score mark
---------------------------------------------------
1 Java 70 pass
2 oracle 90 pass
3 xml 40 fail
4 jsp 30 fail
5 servlet 80 pass
*/
create table scores(
course_id int primary key,
course_name varchar(10),
score int)

insert into scores values(1,'English',45)
insert into scores values(2,'Maths',59)
insert into scores values(3,'Physics',61)
insert into scores values(4,'PE',95)
insert into scores values(5,'Chemistry',84)
insert into scores values(6,'Music',87)

--attention please, case xxx when values then a else b end)
select *,(
case
when score>60
then 'pass'
else 'fail'
end)as mask
from scores

select e.*,d.location_id
from employees e,departments d
where e.department_id = d.department_id

select e.*,d.location_id
from employees e
join departments d
on e.department_id = d.department_id

select * from departments

--查询employees中ID排序的低5条到7条记录
select top 2 * from employees where employee_id <
(select max(employee_id)
from (select top(4) * from employees) as e1)

select * from employees

select max(employee_id)
from (select top(4) * from employees) as e1

select max(employee_id)
from employees


drop table buylogs

create table buylogs(
	book_id int not null primary key,
	book_name varchar(20),
	buy_date varchar(20),
	price int
)

insert into buylogs values(1,'Boy','2010-10-21',23)
insert into buylogs values(2,'Boy1','2011-10-21',23)
insert into buylogs values(3,'Boy2','2000-10-21',23)
insert into buylogs values(4,'Boy3','2000-01-21',23)
insert into buylogs values(5,'Boy4','2018-10-21',23)
insert into buylogs values(6,'Boy5','2013-10-21',23)
insert into buylogs values(7,'Boy6','2020-10-21',23)
insert into buylogs values(8,'Boy2','2000-10-21',23)
insert into buylogs values(9,'Boy3','2000-01-21',23)
insert into buylogs values(10,'Boy4','2018-10-21',23)
insert into buylogs values(11,'Boy5','2013-10-21',23)
insert into buylogs values(12,'Boy6','2020-10-21',23)

create index buydate on buylogs(buy_date,book_name)

drop index buydate on buylogs

select * from buylogs where buy_date>'2000-08-9' and buy_date<'2015'

exec sp_helpindex buylogs

Select CONVERT(varchar(100), GETDATE(), 23)

select CONVERT(varchar, getdate(), 120 )

select CONVERT(varchar(12) , getdate(), 112 )

-- 入学时间为字符型：
-- insert into school.student(学号,专业,入学时间) values(3222,'工商管理',convert(varchar(10),getdate(),120))
-- 入学时间为日期型：
-- insert into school.student(学号,专业,入学时间) values(3222,'工商管理',getdate())


create table testindex(
	name varchar(10),
	sex varchar(10),
	age int
)

insert testindex values('1','2',3)
insert testindex values('1','2',3)
insert testindex values('1','2',4)
insert testindex values('1','3',4)

select * from testindex

create index tt on testindex(name)
create index tt1 on testindex(age)

create clustered index t2 on testindex(age)

drop index tt1 on testindex

create unique index t1 on testindex(name,age)

update testindex set age=12 where name='1'

update testindex set name='2' where sex = '3'
update testindex set name='3 ' where age=4 and sex='2'

sp_helpindex testindex

select dateadd(day,2,'2002-10-02')

select DATEPART(month,'2002-10-12')
select datepart(year,'2003-10-13')
select datepart(hour,'2003-10-3 13:13:23')