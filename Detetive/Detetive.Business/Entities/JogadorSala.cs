﻿using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class JogadorSala : BaseEntity
    {
        public int IdSala { get; set; }
        public int NumeroOrdem { get; set; }
        public int NumeroPassagemSecreta { get; set; }
        public bool VezJogador { get; set; }
        public int? QuantidadeMovimento { get; set; }
        public int CoordenadaLinha { get; set; }
        public int CoordenadaColuna { get; set; }
        public int? IdLocal { get; set; }
        public int IdJogador { get; set; }
        public int? IdSuspeito { get; set; }
        public bool RolouDados { get; set; }
        public virtual Suspeito Suspeito { get; set; }

        internal JogadorSala() : base()
        {
        }

        public JogadorSala(int idJogador, int idSala) : base()
        {
            IdSala = idSala;
            IdJogador = idJogador;
            CoordenadaLinha = 1;
            CoordenadaColuna = 1;
        }

        public bool MinhaVez()
        {
            return VezJogador;
        }

        public bool PossoMeMovimentar(int linhaMovimento, int colunaMovimento)
        {
            int quantidadeMovimentosNecessarios = Math.Abs(CoordenadaLinha - linhaMovimento) +
                                                    Math.Abs(CoordenadaColuna - colunaMovimento);

            return QuantidadeMovimento > 0 && quantidadeMovimentosNecessarios <= QuantidadeMovimento;
        }

        public void Mover(int coordenadaLinha, int coordenadaColuna, int? idLocal = null)
        {
            IdLocal = idLocal;
            CoordenadaLinha = coordenadaLinha;
            CoordenadaColuna = coordenadaColuna;

            int quantidadeMovimentosNecessarios = Math.Abs(CoordenadaLinha - coordenadaLinha) +
                                                    Math.Abs(CoordenadaColuna - coordenadaColuna);

            QuantidadeMovimento -= quantidadeMovimentosNecessarios;
        }

        public void FinalizarTurno(bool fim)
        {
            if (fim)
            {
                VezJogador = fim;
                RolouDados = false;
            }
        }

        public void AlterarCoordenadas(int coordenadaLinha, int coordenadaColuna)
        {
            CoordenadaLinha = coordenadaLinha;
            CoordenadaColuna = coordenadaColuna;
        }

        public void Alterar(JogadorSala jogadorSala)
        {
            NumeroOrdem = jogadorSala.NumeroOrdem;
            NumeroPassagemSecreta = jogadorSala.NumeroPassagemSecreta;
            VezJogador = jogadorSala.VezJogador;
            QuantidadeMovimento = jogadorSala.QuantidadeMovimento;
            CoordenadaLinha = jogadorSala.CoordenadaLinha;
            CoordenadaColuna = jogadorSala.CoordenadaColuna;
            IdLocal = jogadorSala.IdLocal;
            IdSuspeito = jogadorSala.IdSuspeito;
        }

        public void AlterarSuspeito(int? idSuspeito)
        {
            IdSuspeito = idSuspeito;
        }

        public void AlterarQuantidadeMovimento(int quantidadeMovimento)
        {
            QuantidadeMovimento = quantidadeMovimento;
            RolouDados = true;
        }
    }
}