using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// �ƹ����
    /// </summary>
    [DisplayName("�ƹ����")]
    public sealed class Promotion : IId, IStamp
    {
        #region Implementation of IId

        /// <summary>
        /// ��ȡ�������ü�¼���
        /// </summary>
        /// <value>
        /// ��¼���
        /// </value>
        /// <remarks>
        /// ����洢ʱʹ���޷�������
        /// </remarks>
        [DisplayName("��¼���")]
        public long Id { get; set; }

        #endregion

        #region Implementation of ICreateStamp

        /// <summary>
        /// ��ȡ�������ü�¼������
        /// </summary>
        /// <value>
        /// ��¼������
        /// </value>
        /// <remarks>
        /// ����ʹ�ò����˵��˻���
        /// </remarks>
        [DisplayName("��¼������")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// ��ȡ�������ü�¼����ʱ��
        /// </summary>
        /// <value>
        /// ��¼����ʱ��
        /// </value>
        /// <remarks>
        /// ����ʹ�÷�����ʱ�䣬��Ҫʹ��Ĭ�Ͽ�ֵ
        /// </remarks>
        [DisplayName("��¼����ʱ��")]
        public DateTime CreatedAt { get; set; }

        #endregion

        #region Implementation of IUpdateStamp

        /// <summary>
        /// ��ȡ�������ü�¼������
        /// </summary>
        /// <value>
        /// ��¼������
        /// </value>
        /// <remarks>
        /// ����ʹ�ò����˵��˻�����Ϊ��ʱ��ʾ��¼�޸���
        /// </remarks>
        [DisplayName("��¼����޸���")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// ��ȡ�������ü�¼����ʱ��
        /// </summary>
        /// <value>
        /// ��¼����ʱ��
        /// </value>
        /// <remarks>
        /// Ϊ��ʱ��ʾ��¼�޸���
        /// </remarks>
        [DisplayName("��¼����޸�ʱ��")]
        public DateTime? UpdatedAt { get; set; }

        #endregion

        /// <summary>
        /// ��ȡ����������Ŀ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ŀ���")]
        [StringLength(20)]
        [Required]
        public string DpNo { get; set; }
        
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ����������Ч����ʼ
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ч����ʼ")]
        public DateTime BeginAt { get; set; }

        /// <summary>
        /// ��ȡ����������Ч�ڽ�ֹ
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ч�ڽ�ֹ")]
        public DateTime EndAt { get; set; }

        /// <summary>
        /// ��ȡ��������Ӷ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("Ӷ��")]
        public int Brokerage { get; set; }

        /// <summary>
        /// ��ȡ���������Ƽ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�Ƽ�����")]
        public int TuijianCount { get; set; }

        /// <summary>
        /// ��ȡ���������Ƽ���Ч����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�Ƽ���Ч����")]
        public int TuijianValidCount { get; set; }

        /// <summary>
        /// ��ȡ���������ѵ�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ѵ�������")]
        public int DaofangCount { get; set; }

        /// <summary>
        /// ��ȡ�����������ϳ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("���ϳ�����")]
        public int RenchouCount { get; set; }

        /// <summary>
        /// ��ȡ���������ѳɽ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ѳɽ�����")]
        public int ChengjiaoCount { get; set; }

        /// <summary>
        /// ��ȡ��������ͳ�Ƹ���ʱ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("ͳ�Ƹ���ʱ��")]
        public DateTime SummaryAt { get; set; }

    }
}
