# Zack.EFCore.Batch
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
