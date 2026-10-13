using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoX.Models
{
	[Table("registros")]
	public class Log
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("game_state")]
		public string GameState { get; set; } = string.Empty;

		[Column("review")]
		public string Review { get; set; } = string.Empty;

		[Column("rating")]
		public int Rating { get; set; }

		[Column("user_id")]
		public int UserId { get; set; }

		[Column("game_id")]
		public int GameId { get; set; }

		[Column("log_date")]
		public DateTime LogDate { get; set; }
	}
}
