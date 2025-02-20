﻿using System;
using System.Collections.Generic;

namespace BH_Estética_VisualStudio.ORM;

public partial class ViewAgendamento
{
    public int Id { get; set; }

    public DateTime DtHoraAgendamento { get; set; }

    public DateOnly DataAtendimento { get; set; }

    public TimeOnly Horario { get; set; }

    public string TipoServico { get; set; } = null!;

    public decimal Valor { get; set; }

    public string Nome { get; set; } = null!;

    public string? Email { get; set; }

    public string Telefone { get; set; } = null!;
}
