using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pruebas.Cliente.Models;

public partial class SigaFfmContext : DbContext
{
    public SigaFfmContext()
    {
    }

    public SigaFfmContext(DbContextOptions<SigaFfmContext> options)
        : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    // => optionsBuilder.UseSqlServer("workstation id=SIGA_FFM.mssql.somee.com;packet size=4096;user id=aperezNWOGmail_SQLLogin_1;pwd=4tvg3todsw;data source=SIGA_FFM.mssql.somee.com;persist security info=False;initial catalog=SIGA_FFM;TrustServerCertificate=True");
    => optionsBuilder.UseSqlServer("workstation id=SIGA_FFMM.mssql.somee.com;packet size=4096;user id=aperezNWOOutlook_SQLLogin_1;pwd=vtl7mbj91h;data source=SIGA_FFMM.mssql.somee.com;persist security info=False;initial catalog=SIGA_FFMM;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
