# Zack.EFCore.Batch
.NET 7��ʼ��EF Core�Ѿ������˶�����ɾ�����������µ�֧�֣���˱���Ŀ������.NET7��֧�����������ܣ�[����������](https://learn.microsoft.com/zh-cn/ef/core/what-is-new/ef-core-7.0/whatsnew?WT.mc_id=DT-MVP-5004444#executeupdate-and-executedelete-bulk-updates)�������Ǳ���Ŀ��Ȼ��.NET 7��֧�����ݵ��������롣

ʹ�����������, Entity Framework Core �û����Կ��������������ݡ�
���������֧�� Entity Framework Core 7�����ϰ汾��  

## ����˵��:  

### ��װNuget����
```
SQLServer: Install-Package Zack.EFCore.Batch.MSSQL_NET7
MySQL: Install-Package Zack.EFCore.Batch.MySQL.Pomelo_NET7
Postgresql: Install-Package Zack.EFCore.Batch.Npgsql_NET7
```

### ������������
```
List<Book> books = new List<Book>();
for (int i = 0; i < 100; i++)
{
	books.Add(new Book { AuthorName = "abc" + i, Price = new Random().NextDouble(), PubTime = DateTime.Now, Title = Guid.NewGuid().ToString() });
}
using (TestDbContext ctx = new TestDbContext())
{
	ctx.BulkInsert(books);
}
```
�� mysql��, ���ʹ��BulkInsert�����ڷ������˺Ϳͻ��˶�����local_infile����mysql server������������"local_infile=ON"��Ȼ���������ַ�������� "AllowLoadLocalInfile=true"��
